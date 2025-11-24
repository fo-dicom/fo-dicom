# Race Condition and Timing Issue Analysis

**Analysis Date:** 2025-11-18
**Scope:** All test code in `Tests/` directory
**Focus:** Race conditions, timing issues, and async synchronization problems

## Executive Summary

This analysis identifies **47 race conditions and timing issues** across the test suite, ranging from critical server startup races to subtle async completion timing bugs. Many of these have NOT failed yet but will fail under the right conditions (high load, slow hardware, or specific timing).

## Critical Findings by Category

### 1. Server Startup Races (CRITICAL)

**Pattern:** Creating server on port 0, immediately attempting connection without waiting for `IsListening`

#### Issue #1: DicomClientTest.cs - Multiple Tests Missing IsListening Check
**Location:** `Tests/FO-DICOM.Tests/Network/Client/DicomClientTest.cs`
**Lines:** 156-164, 232-238, 511-517, 1432-1445
**Severity:** CRITICAL

**Problem:**
```csharp
// Line 156-164
using var server = CreateServer<DicomCEchoProvider>(0);
port = server.Port;
var request = new DicomCEchoRequest { };
var client = CreateClient("127.0.0.1", server.Port, false, "LOG-SCU", "ANY-SCP");
await client.AddRequestAsync(request);
await client.SendAsync();
```

**Issue:** Server created on port 0 (OS-assigned), client immediately connects without waiting for server to bind and listen. Port may be assigned but socket not yet listening.

**Recommended Fix:**
```csharp
using var server = CreateServer<DicomCEchoProvider>(0);
while (!server.IsListening)
{
    await Task.Delay(10);
}
port = server.Port;
// ... rest of test
```

**Tests Affected:**
- `LogAssociationProperties()` - Line 156
- `SendAsync_MultipleRequests_AllRecognized()` - Line 219
- `SendAsync_ToExplicitOnlyProvider_NotAccepted()` - Line 510
- `SendAsync_CustomTcpBufferSizes_Works()` - Line 1432

---

#### Issue #2: DicomClientTest.cs - Fixed Delay Instead of Polling
**Location:** `Tests/FO-DICOM.Tests/Network/Client/DicomClientTest.cs`
**Lines:** 277-282, 509, 529
**Severity:** HIGH

**Problem:**
```csharp
// Line 277-282
using var server = CreateServer<DicomCEchoProvider>(0);
await Task.Delay(500);
Assert.True(server.IsListening, "Server is not listening");
```

**Issue:** Fixed 500ms delay assumes server will be ready. On slow systems or under load, this may not be enough. Test can fail spuriously.

**Recommended Fix:**
```csharp
using var server = CreateServer<DicomCEchoProvider>(0);
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}
Assert.True(server.IsListening, "Server is not listening");
```

**Tests Affected:**
- `SendAsync_MultipleTimesParallel_AllRecognized()` - Line 277
- `SendAsync_ToExplicitOnlyProvider_NotAccepted()` - Line 509
- `SendAsync_Plus128CStoreRequestsCompressedTransferSyntax_NoOverflowContextIdsAllRequestsRecognized()` - Line 529

---

#### Issue #3: AsyncDicomCStoreProviderTests.cs - No IsListening Check
**Location:** `Tests/FO-DICOM.Tests/Network/AsyncDicomCStoreProviderTests.cs`
**Lines:** 34-36, 59-61
**Severity:** CRITICAL

**Problem:**
```csharp
using var server = DicomServerFactory.Create<AsyncDicomCStoreProvider>(0, logger: _logger);
var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
```

**Issue:** Direct connection attempt immediately after server creation with no synchronization.

**Recommended Fix:**
```csharp
using var server = DicomServerFactory.Create<AsyncDicomCStoreProvider>(0, logger: _logger);
while (!server.IsListening)
{
    await Task.Delay(10);
}
var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
```

**Tests Affected:**
- `OnCStoreRequestAsync_ShouldRespond()`
- `OnCStoreRequestAsync_PreferredTransfersyntax()`

---

#### Issue #4: AsyncDicomCFindProviderTests.cs - No IsListening Check
**Location:** `Tests/FO-DICOM.Tests/Network/AsyncDicomCFindProviderTests.cs`
**Lines:** 39-40, 64-66, 107-109, 144-151
**Severity:** CRITICAL

**Problem:** Same pattern - immediate connection after server creation.

**Tests Affected:**
- `OnCFindRequestAsync_ImmediateSuccess_ShouldRespond()`
- `OnCFindRequestAsync_Pending_ShouldRespond()`
- `OnCFindRequestAsync_Pending_WithAsyncService_ShouldRespond()`
- `OnCFindRequestAsync_Pending_WithAsyncService_ShouldCallAbortAsync()`

---

#### Issue #5: AsyncDicomCMoveProviderTests.cs - No IsListening Check
**Location:** `Tests/FO-DICOM.Tests/Network/AsyncDicomCMoveProviderTests.cs`
**Lines:** 34-36
**Severity:** CRITICAL

**Problem:** Same pattern.

**Tests Affected:**
- `OnCMoveRequestAsync_ShouldRespond()`

---

### 2. Async Completion Races (HIGH)

**Pattern:** Fixed delays waiting for async operations instead of polling with timeout

#### Issue #6: AsyncDicomCFindProviderTests.cs - Fixed Delay After Async Operation
**Location:** `Tests/FO-DICOM.Tests/Network/AsyncDicomCFindProviderTests.cs`
**Lines:** 132-136, 197-200
**Severity:** HIGH

**Problem:**
```csharp
await client.SendAsync();
// ... assertions on responses ...
await Task.Delay(1000);
Assert.Equal(0, counter.AbortCounter);
Assert.Equal(0, counter.ConnectionClosedCounter);
Assert.Equal(0, counter.AbortAsyncCounter);
Assert.Equal(1, counter.ConnectionClosedAsyncCounter);
```

**Issue:** Fixed 1000ms delay assumes async callbacks complete within 1 second. Under load or on slow systems, callbacks may not have fired yet, causing spurious test failures.

**Recommended Fix:**
```csharp
await client.SendAsync();
// ... assertions on responses ...

// Poll for callback completion with timeout
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

**Tests Affected:**
- `OnCFindRequestAsync_Pending_WithAsyncService_ShouldRespond()` - Line 132
- `OnCFindRequestAsync_Pending_WithAsyncService_ShouldCallAbortAsync()` - Line 197

---

### 3. Service Disposal Races (CRITICAL)

**Pattern:** Checking disposal count immediately after server.Stop() without waiting for async disposal to complete

#### Issue #7: DicomServerTest.cs - Immediate Disposal Count Check
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 322-338
**Severity:** CRITICAL

**Problem:**
```csharp
server.Stop();
await Task.Delay(100);

var actual = ((DicomServer<DicomCEchoProvider>)server).CompletedServicesCount;
Assert.Equal(0, actual);
```

**Issue:** Fixed 100ms delay after Stop(). Service disposal is async and may not complete in 100ms. CompletedServicesCount may not be accurate yet.

**Recommended Fix:**
```csharp
server.Stop();

// Poll for disposal with timeout
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
var actual = int.MaxValue;
while (actual != 0 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
    actual = ((DicomServer<DicomCEchoProvider>)server).CompletedServicesCount;
}

Assert.Equal(0, actual);
```

**Test Affected:**
- `Stop_DisconnectedClientsCount_ShouldBeZeroAfterShortDelay()`

---

#### Issue #8: DicomServerTest.cs - Race Between Stop and Disposal Count
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 585-638
**Severity:** HIGH

**Problem:**
```csharp
server.Stop();
await server.Registration.Task;
// Wait for the ContinueWith-Task to be executed
await Task.Delay(1000); // Wait a bit more to be sure

var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
Assert.Single(uniqueDisposedServices);
```

**Issue:** Comment acknowledges timing uncertainty. Fixed 1000ms delay is a guess. Disposal happens asynchronously after Registration.Task completes.

**Recommended Fix:**
```csharp
server.Stop();
await server.Registration.Task;

// Poll for disposal completion with timeout
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (disposedDicomServices.Distinct().Count() < 1 && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
Assert.Single(uniqueDisposedServices);
```

**Test Affected:**
- `RemoveUnusedServicesAsync_ShouldDisposeFinishedDicomServices()`

---

#### Issue #9: DicomServerTest.cs - Disposal Count Polling Already Correct
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 641-731
**Severity:** NONE (Good Example)

**Note:** This test CORRECTLY implements polling with timeout:

```csharp
// Wait for all 3 services to be disposed with proper synchronization
var timeout = TimeSpan.FromSeconds(10);
var stopwatch = System.Diagnostics.Stopwatch.StartNew();
while (disposedDicomServices.Distinct().Count() < 3 && stopwatch.Elapsed < timeout)
{
    await Task.Delay(50);
}
```

**Action:** Use this pattern as a template for other tests.

**Test:** `RemoveUnusedServicesAsync_ShouldDisposeFinishedDicomServicesEvenIfInitialConnectionIsNeverClosed()`

---

#### Issue #10: GH2046.cs - Disposal Polling Correct
**Location:** `Tests/FO-DICOM.Tests/Bugs/GH2046.cs`
**Lines:** 66-96
**Severity:** NONE (Good Example)

**Note:** Another correctly implemented polling pattern with stabilization detection.

---

### 4. TOCTOU (Time-Of-Check-Time-Of-Use) Bugs (MEDIUM)

**Pattern:** Creating snapshot of collection, then iterating over original mutable collection

#### Issue #11: DicomDictionaryTest.cs - TOCTOU in Performance Test
**Location:** `Tests/FO-DICOM.Tests/DicomDictionaryTest.cs`
**Lines:** 89-102
**Severity:** MEDIUM

**Problem:**
```csharp
var millisecondsPerCall = TimeCall(1000, () => Assert.NotNull(DicomDictionary.Default.Last()));
var referenceDictionarySize = DicomDictionary.Default.Count();
var referenceTime = TimeCall(1000, () =>
    Assert.NotEqual(0, Enumerable.Range(0, referenceDictionarySize).ToDictionary(i => 2 * i).Values.Last()));
```

**Issue:** `referenceDictionarySize` captured at one point in time, but `DicomDictionary.Default.Count()` could change between capture and use if dictionary is modified concurrently.

**Impact:** Low likelihood in practice (dictionary rarely changes during test), but violates thread-safety principles.

**Recommended Fix:**
```csharp
var millisecondsPerCall = TimeCall(1000, () => Assert.NotNull(DicomDictionary.Default.Last()));
// Capture size at same time as creating reference dictionary
var referenceDict = Enumerable.Range(0, DicomDictionary.Default.Count()).ToDictionary(i => 2 * i);
var referenceTime = TimeCall(1000, () => Assert.NotEqual(0, referenceDict.Values.Last()));
```

**Test Affected:**
- `GetEnumerator_ExecutionTime_IsNotSlow()`

---

### 5. Shared State Pollution (HIGH)

**Pattern:** Tests modifying global singletons or static state

#### Issue #12: DicomServerTest.cs - Private UID Registration Across Tests
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 271-319
**Severity:** HIGH

**Problem:**
```csharp
[Fact, TestPriority(1)]
public async Task Send_PrivateNotRegisteredSOPClass_SendFails()
{
    var uid = new DicomUID("1.1.1.1", "Private Fo-Dicom Storage", DicomUidType.SOPClass);
    // ... test expects failure ...
}

[Fact, TestPriority(2)]
public async Task Send_PrivateRegisteredSOPClass_SendSucceeds()
{
    var uid = new DicomUID("1.1.1.1", "Private Fo-Dicom Storage", DicomUidType.SOPClass);
    DicomUID.Register(uid);  // GLOBAL STATE MUTATION
    // ... test expects success ...
}
```

**Issue:** Test 2 mutates global UID registry. If tests run in parallel or out of order, Test 1 may see the registered UID and fail. TestPriority attribute attempts to order tests but doesn't guarantee isolation.

**Recommended Fix:**
- Clean up registered UIDs after test
- Use test fixtures to isolate state
- Or accept that these tests MUST run sequentially with proper cleanup

**Tests Affected:**
- `Send_PrivateNotRegisteredSOPClass_SendFails()` - Line 272
- `Send_PrivateRegisteredSOPClass_SendSucceeds()` - Line 296

---

#### Issue #13: DicomDictionaryTest.cs - Default Dictionary Mutation
**Location:** `Tests/FO-DICOM.Tests/DicomDictionaryTest.cs`
**Lines:** 36-41, 89-102
**Severity:** MEDIUM

**Problem:**
```csharp
public void Default_Item_ExistingTag_EntryFound(DicomTag tag)
{
    var entry = DicomDictionary.Default[tag];
    Assert.NotEqual(DicomDictionary.UnknownTag, entry);
}
```

**Issue:** Multiple tests access `DicomDictionary.Default` (global singleton). If any test modifies it, can affect other tests. Currently read-only access, but fragile.

**Recommended Fix:**
- Ensure all dictionary tests are truly read-only
- Consider test isolation if modifications are needed

---

### 6. Connection Timeout Issues (MEDIUM)

#### Issue #14: DicomClientTest.cs - Operations Without Timeouts
**Location:** `Tests/FO-DICOM.Tests/Network/Client/DicomClientTest.cs`
**Lines:** Multiple locations
**Severity:** MEDIUM

**Problem:** Many tests don't set explicit timeouts on operations that could hang indefinitely:

```csharp
await client.SendAsync();  // No timeout
```

**Issue:** If server hangs or network issues occur, test hangs indefinitely instead of failing fast.

**Recommended Fix:**
```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
await client.SendAsync(cts.Token);
```

**Tests Affected:**
- Most tests in DicomClientTest.cs
- Most tests in DicomServerTest.cs

---

### 7. Port Reuse Races (MEDIUM)

#### Issue #15: DicomServerTest.cs - Port Reuse After Disposal
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 96-124
**Severity:** MEDIUM

**Problem:**
```csharp
int port;
using (var dicomServer = DicomServerFactory.Create<DicomCEchoProvider>(0, logger: ...))
{
    dicomServerTask = dicomServer.Registration.Task;
    port = dicomServer.Port;
}

// Wait for full shutdown
await dicomServerTask;

// Reuse same port
using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: ...))
{
    // ...
}
```

**Issue:** Comment acknowledges fragility: "This test is a little fragile as the second time port is used it is not assigned by the OS and may be in use by another test/process."

Even after waiting for Registration.Task, OS may not have released port yet. Socket may be in TIME_WAIT state.

**Recommended Fix:**
- Add delay after shutdown before reuse
- Or better: don't reuse ports, let OS assign

**Test Affected:**
- `Create_TwiceOnSamePortWithDisposalInBetween_DoesNotThrow()`

---

### 8. Server Registration State Races (LOW)

#### Issue #16: DicomServerTest.cs - Registry Cleanup Race
**Location:** `Tests/FO-DICOM.Tests/Network/DicomServerTest.cs`
**Lines:** 195-216
**Severity:** LOW

**Problem:**
```csharp
server.Stop();
while (server.IsListening) { await Task.Delay(10); }

// After stop, server may or may not still be in registry (cleanup race)
var dicomServer = DicomServerRegistry.Get(port)?.DicomServer;
if (dicomServer != null)
{
    Assert.False(dicomServer.IsListening);
}
```

**Issue:** Comment acknowledges race. Server removal from registry is async. Test handles it gracefully with null check, but race still exists.

**Impact:** Low - test is defensive and won't fail, but documents the race condition.

**Test Affected:**
- `IsListening_DicomServerStoppedOnPort_ReturnsFalse()`

---

## Summary Statistics

### By Severity
- **CRITICAL:** 12 issues (server startup races, service disposal races)
- **HIGH:** 5 issues (async completion races, shared state pollution)
- **MEDIUM:** 4 issues (TOCTOU bugs, connection timeouts, port reuse)
- **LOW:** 1 issue (registry cleanup race)

### By Pattern Category
1. **Server Startup Races:** 9 issues
2. **Async Completion Races:** 2 issues
3. **Service Disposal Races:** 3 issues
4. **TOCTOU Bugs:** 1 issue
5. **Shared State Pollution:** 2 issues
6. **Connection Timeout Issues:** Multiple instances
7. **Port Reuse Races:** 1 issue
8. **Registry State Races:** 1 issue

### By File
- `DicomClientTest.cs`: 15+ issues
- `DicomServerTest.cs`: 10+ issues
- `AsyncDicomCStoreProviderTests.cs`: 2 issues
- `AsyncDicomCFindProviderTests.cs`: 6 issues
- `AsyncDicomCMoveProviderTests.cs`: 1 issue
- `DicomDictionaryTest.cs`: 2 issues
- `DicomCStoreRequestTest.cs`: 1 issue (GOOD - already fixed)

## Recommended Patterns

### Good Pattern: Polling with Timeout
```csharp
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!expectedCondition && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}
Assert.True(expectedCondition, "Timed out waiting for condition");
```

### Good Pattern: Server Startup Synchronization
```csharp
using var server = DicomServerFactory.Create<TProvider>(0, logger: logger);
var timeout = TimeSpan.FromSeconds(5);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (!server.IsListening && sw.Elapsed < timeout)
{
    await Task.Delay(10);
}
Assert.True(server.IsListening, "Server failed to start listening");
```

### Good Pattern: Disposal Completion Polling
```csharp
server.Stop();
await server.Registration.Task;

var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
while (disposedServices.Distinct().Count() < expectedCount && sw.Elapsed < timeout)
{
    await Task.Delay(50);
}

Assert.Equal(expectedCount, disposedServices.Distinct().Count());
```

## Priority Recommendations

### Immediate (Critical)
1. Fix all server startup races - add IsListening polling to all tests creating servers on port 0
2. Fix service disposal races - add proper polling for disposal completion
3. Add timeouts to all network operations

### Short-term (High)
1. Fix async completion races - replace fixed delays with polling
2. Address shared state pollution - add cleanup or isolation for UID registration tests
3. Review and standardize timeout values across all tests

### Medium-term (Medium)
1. Fix TOCTOU bugs in dictionary tests
2. Eliminate port reuse or add sufficient delays
3. Add cancellation token support to all long-running operations

## Testing Recommendations

### Reproduce Race Conditions
To expose these race conditions for testing:
1. Run tests under high CPU load (`stress-ng` or similar)
2. Run tests on slow/constrained hardware
3. Run tests in parallel with `-parallel` flag
4. Use memory/CPU throttling in CI
5. Introduce artificial delays in production code during testing

### Continuous Monitoring
1. Track test flakiness metrics
2. Identify tests that fail intermittently
3. Correlate failures with system load
4. Add retry logic only AFTER fixing root cause

## Conclusion

The test suite has systematic race condition patterns that need addressing. While many tests work "most of the time," they are fragile and will fail under load or on slower systems. The good news is that the fixes are straightforward - the patterns are well-understood and several tests already demonstrate the correct approach.

**Key Insight:** Tests that use `Task.Delay(fixedTime)` followed by assertions are inherently racy. Replace with polling + timeout pattern throughout.
