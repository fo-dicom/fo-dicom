// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Helpers
{
    /// <summary>
    /// Helper methods for async test synchronization to avoid race conditions.
    /// </summary>
    internal static class AsyncTestHelper
    {

        /// <summary>
        /// Waits for a condition to become true with timeout and polling.
        /// </summary>
        /// <param name="condition">Function that returns true when condition is met</param>
        /// <param name="timeoutSeconds">Timeout in seconds (default 10)</param>
        /// <param name="failureMessage">Message to show if condition not met within timeout</param>
        /// <param name="pollingIntervalMs">Polling interval in milliseconds (default 50)</param>
        public static async Task AssertConditionAsync(
            Func<bool> condition,
            int timeoutSeconds = 10,
            string failureMessage = "Condition was not met within timeout",
            int pollingIntervalMs = 50)
        {
            var timeout = TimeSpan.FromSeconds(timeoutSeconds);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (!condition() && stopwatch.Elapsed < timeout)
            {
                await Task.Delay(pollingIntervalMs).ConfigureAwait(false);
            }

            Assert.True(condition(), failureMessage);
        }


        /// <summary>
        /// Waits for a count to reach the expected value with timeout and polling.
        /// Useful for waiting for disposal counts, response counts, etc.
        /// </summary>
        public static async Task<bool> WaitForCountAsync(
            Func<int> getCount,
            int expectedCount,
            TimeSpan? timeout = null,
            int pollingIntervalMs = 50)
        {
            timeout ??= TimeSpan.FromSeconds(10);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (getCount() < expectedCount && stopwatch.Elapsed < timeout)
            {
                await Task.Delay(pollingIntervalMs);
            }

            var actualCount = getCount();
            return actualCount == expectedCount;
        }

        /// <summary>
        /// Waits for server to start listening on its assigned port.
        /// Essential for tests using port 0 (OS-assigned ports).
        /// </summary>
        public static Task WaitForServerListeningAsync(
            IDicomServer server,
            int timeoutSeconds = 5)
            => AssertConditionAsync(
                () => server.IsListening,
                timeoutSeconds,
                $"Server on port {server.Port} failed to start listening within {timeoutSeconds}s",
                pollingIntervalMs: 10);
    }
}
