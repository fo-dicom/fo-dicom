# Race Condition Fixes - Action Plan

## Quick Reference: Files Needing Immediate Attention

### Critical Priority (Fix First)

1. **Tests/FO-DICOM.Tests/Network/Client/DicomClientTest.cs**
   - 15+ race conditions
   - Missing IsListening checks
   - Fixed delays instead of polling

2. **Tests/FO-DICOM.Tests/Network/DicomServerTest.cs**
   - 10+ race conditions
   - Service disposal races
   - Port reuse issues

3. **Tests/FO-DICOM.Tests/Network/AsyncDicomCStoreProviderTests.cs**
   - 2 critical server startup races

4. **Tests/FO-DICOM.Tests/Network/AsyncDicomCFindProviderTests.cs**
   - 6 issues (startup + async completion races)

5. **Tests/FO-DICOM.Tests/Network/AsyncDicomCMoveProviderTests.cs**
   - 1 server startup race

---

## Pattern-Based Fixes

### Pattern 1: Server Startup Without IsListening Check

**Find:** `DicomServerFactory.Create<.*>\(0,` followed by immediate usage

**Fix Template:**
```csharp
// BEFORE (WRONG):
using var server = DicomServerFactory.Create<TProvider>(0, logger: logger);
var client = DicomClientFactory.Create("127.0.0.1", server.Port, ...);

// AFTER (CORRECT):
using var server = DicomServerFactory.Create<TProvider>(0, logger: logger);
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(10);
}
Assert.True(server.IsListening, "Server failed to start listening within timeout");
var client = DicomClientFactory.Create("127.0.0.1", server.Port, ...);
```

**Apply to:**
- DicomClientTest.cs: Lines 156, 219, 510, 1432
- AsyncDicomCStoreProviderTests.cs: Lines 34, 59
- AsyncDicomCFindProviderTests.cs: Lines 39, 64, 107, 144
- AsyncDicomCMoveProviderTests.cs: Line 34

---

### Pattern 2: Fixed Delay Instead of IsListening Poll

**Find:** `await Task.Delay(\d+);` followed by `Assert.True(server.IsListening`

**Fix Template:**
```csharp
// BEFORE (WRONG):
using var server = CreateServer<DicomCEchoProvider>(0);
await Task.Delay(500);
Assert.True(server.IsListening, "Server is not listening");

// AFTER (CORRECT):
using var server = CreateServer<DicomCEchoProvider>(0);
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}
Assert.True(server.IsListening, "Server failed to start listening within timeout");
```

**Apply to:**
- DicomClientTest.cs: Lines 277, 509, 529

---

### Pattern 3: Fixed Delay After Async Operation

**Find:** `await .*SendAsync\(\);` followed by assertions, then `await Task.Delay\(\d+\);` then counter assertions

**Fix Template:**
```csharp
// BEFORE (WRONG):
await client.SendAsync();
// ... response assertions ...
await Task.Delay(1000);
Assert.Equal(expected, counter.SomeValue);

// AFTER (CORRECT):
await client.SendAsync();
// ... response assertions ...

// Poll for async callback completion
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (counter.SomeValue != expected && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}
Assert.Equal(expected, counter.SomeValue);
```

**Apply to:**
- AsyncDicomCFindProviderTests.cs: Lines 132, 197

---

### Pattern 4: Service Disposal Without Polling

**Find:** `server.Stop\(\);` followed by fixed delay then disposal count assertion

**Fix Template:**
```csharp
// BEFORE (WRONG):
server.Stop();
await Task.Delay(100);
var actual = server.CompletedServicesCount;
Assert.Equal(0, actual);

// AFTER (CORRECT):
server.Stop();

// Poll for disposal completion
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
var actual = int.MaxValue;
while (actual != 0 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
    actual = server.CompletedServicesCount;
}
Assert.Equal(0, actual);
```

**Apply to:**
- DicomServerTest.cs: Line 322

---

### Pattern 5: Disposal Count Race After Registration.Task

**Find:** `await server.Registration.Task;` followed by fixed delay then disposal assertions

**Fix Template:**
```csharp
// BEFORE (WRONG):
server.Stop();
await server.Registration.Task;
await Task.Delay(1000);
var count = disposedServices.Distinct().Count();
Assert.Equal(expected, count);

// AFTER (CORRECT):
server.Stop();
await server.Registration.Task;

// Poll for disposal completion with timeout
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (disposedServices.Distinct().Count() < expected && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

var count = disposedServices.Distinct().Count();
Assert.Equal(expected, count);
```

**Apply to:**
- DicomServerTest.cs: Line 585

---

## File-by-File Action Items

### DicomClientTest.cs

**Line 156 - LogAssociationProperties()**
```csharp
// ADD after server creation:
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(10);
}
Assert.True(server.IsListening);
```

**Line 219 - SendAsync_MultipleRequests_AllRecognized()**
```csharp
// ADD after server creation:
while (!server.IsListening) { await Task.Delay(10); }
```

**Line 277 - SendAsync_MultipleTimesParallel_AllRecognized()**
```csharp
// REPLACE fixed delay with polling:
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}
Assert.True(server.IsListening, "Server is not listening");
```

**Line 509 - SendAsync_Plus128CStoreRequestsCompressedTransferSyntax_NoOverflowContextIdsAllRequestsRecognized()**
```csharp
// REPLACE:
await Task.Delay(100);
// WITH:
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(10);
}
Assert.True(server.IsListening, "Server failed to start");
```

**Line 510 - SendAsync_ToExplicitOnlyProvider_NotAccepted()**
```csharp
// ADD after server creation:
while (!server.IsListening) { await Task.Delay(10); }
```

**Line 1432 - SendAsync_CustomTcpBufferSizes_Works()**
```csharp
// ADD after server creation:
while (!server.IsListening) { await Task.Delay(10); }
```

---

### DicomServerTest.cs

**Line 322-338 - Stop_DisconnectedClientsCount_ShouldBeZeroAfterShortDelay()**
```csharp
// REPLACE:
server.Stop();
await Task.Delay(100);

var actual = ((DicomServer<DicomCEchoProvider>)server).CompletedServicesCount;
Assert.Equal(0, actual);

// WITH:
server.Stop();

// Poll for disposal completion
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
int actual;
do
{
    await Task.Delay(50);
    actual = ((DicomServer<DicomCEchoProvider>)server).CompletedServicesCount;
} while (actual != 0 && sw.Elapsed < timeout);

Assert.Equal(0, actual);
```

**Line 585-638 - RemoveUnusedServicesAsync_ShouldDisposeFinishedDicomServices()**
```csharp
// REPLACE:
await Task.Delay(1000);

var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
Assert.Single(uniqueDisposedServices);

// WITH:
// Poll for disposal completion
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (disposedDicomServices.Distinct().Count() < 1 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
Assert.Single(uniqueDisposedServices);
```

---

### AsyncDicomCStoreProviderTests.cs

**Line 34-36 - OnCStoreRequestAsync_ShouldRespond()**
```csharp
// ADD after server creation:
using var server = DicomServerFactory.Create<AsyncDicomCStoreProvider>(0, logger: _logger.IncludePrefix("DicomServer"));
while (!server.IsListening) { await Task.Delay(10); }
var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
```

**Line 59-61 - OnCStoreRequestAsync_PreferredTransfersyntax()**
```csharp
// ADD after server creation:
using var server = DicomServerFactory.Create<AsyncDicomCStoreProviderPreferingUncompressedTS>(0, logger: _logger.IncludePrefix("DicomServer"));
while (!server.IsListening) { await Task.Delay(10); }
var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
```

---

### AsyncDicomCFindProviderTests.cs

**Lines 39-40, 64-66, 107-109 - All server creations**
```csharp
// ADD after each server creation:
while (!server.IsListening) { await Task.Delay(10); }
```

**Line 132-136 - OnCFindRequestAsync_Pending_WithAsyncService_ShouldRespond()**
```csharp
// REPLACE:
await Task.Delay(1000);
Assert.Equal(0, counter.AbortCounter);
Assert.Equal(0, counter.ConnectionClosedCounter);
Assert.Equal(0, counter.AbortAsyncCounter);
Assert.Equal(1, counter.ConnectionClosedAsyncCounter);

// WITH:
// Poll for async callback completion
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (counter.ConnectionClosedAsyncCounter == 0 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

Assert.Equal(0, counter.AbortCounter);
Assert.Equal(0, counter.ConnectionClosedCounter);
Assert.Equal(0, counter.AbortAsyncCounter);
Assert.Equal(1, counter.ConnectionClosedAsyncCounter);
```

**Line 197-200 - OnCFindRequestAsync_Pending_WithAsyncService_ShouldCallAbortAsync()**
```csharp
// REPLACE:
await Task.Delay(1000);
Assert.Equal(0, counter.AbortCounter);
Assert.Equal(0, counter.ConnectionClosedCounter);
Assert.Equal(1, counter.AbortAsyncCounter);

// WITH:
// Poll for async callback completion
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (counter.AbortAsyncCounter == 0 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

Assert.Equal(0, counter.AbortCounter);
Assert.Equal(0, counter.ConnectionClosedCounter);
Assert.Equal(1, counter.AbortAsyncCounter);
```

---

### AsyncDicomCMoveProviderTests.cs

**Line 34 - OnCMoveRequestAsync_ShouldRespond()**
```csharp
// ADD after server creation:
using var server = DicomServerFactory.Create<AsyncDicomCMoveProvider>(0, logger: _logger.IncludePrefix("DicomServer"));
while (!server.IsListening) { await Task.Delay(10); }
var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
```

---

## Verification Steps

After applying fixes:

1. **Run affected tests 100 times:**
   ```bash
   for i in {1..100}; do
     dotnet test --filter "FullyQualifiedName~DicomClientTest" --logger "console;verbosity=minimal"
   done
   ```

2. **Run under CPU stress:**
   ```bash
   stress-ng --cpu 8 --timeout 60s &
   dotnet test --filter "FullyQualifiedName~Network"
   ```

3. **Run with artificial delays:**
   Add environment variable to inject random delays in production code during test runs.

4. **Check for remaining Task.Delay patterns:**
   ```bash
   grep -n "await Task.Delay" Tests/FO-DICOM.Tests/Network/**/*.cs | \
     grep -v "while\|for\|stopwatch\|timeout"
   ```

---

## Low-Priority Items

### Shared State (Clean up after fixing critical issues)

**DicomServerTest.cs - Lines 271-319**
```csharp
[Fact, TestPriority(2)]
public async Task Send_PrivateRegisteredSOPClass_SendSucceeds()
{
    var uid = new DicomUID("1.1.1.1", "Private Fo-Dicom Storage", DicomUidType.SOPClass);
    try
    {
        DicomUID.Register(uid);
        // ... test logic ...
    }
    finally
    {
        // ADD CLEANUP:
        DicomUID.Unregister(uid); // If such method exists
        // OR reset dictionary state
    }
}
```

### TOCTOU Issues

**DicomDictionaryTest.cs - Line 96**
```csharp
// REPLACE:
var referenceDictionarySize = DicomDictionary.Default.Count();
var referenceTime = TimeCall(1000, () =>
    Assert.NotEqual(0, Enumerable.Range(0, referenceDictionarySize).ToDictionary(i => 2 * i).Values.Last()));

// WITH:
var referenceDict = Enumerable.Range(0, DicomDictionary.Default.Count()).ToDictionary(i => 2 * i);
var referenceTime = TimeCall(1000, () =>
    Assert.NotEqual(0, referenceDict.Values.Last()));
```

---

## Testing Strategy

### Phase 1: Critical Fixes (Do First)
- Fix all server startup races
- Fix service disposal races
- Verify each fix individually

### Phase 2: High-Priority Fixes
- Fix async completion races
- Add operation timeouts
- Verify fixes don't break existing tests

### Phase 3: Medium-Priority Fixes
- Address TOCTOU bugs
- Fix shared state pollution
- Add cleanup where needed

### Phase 4: Validation
- Run full test suite 1000 times
- Run under various stress conditions
- Document any remaining flaky tests

---

## Success Criteria

- [ ] Zero test failures in 100 consecutive runs
- [ ] Zero failures under CPU stress
- [ ] All `Task.Delay` patterns reviewed and justified
- [ ] No remaining server startup races
- [ ] All disposal operations use polling
- [ ] All network operations have timeouts
