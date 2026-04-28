// Copyright (c) 2012-2025 fo-dicom contributors.
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
    public static class AsyncTestHelper
    {
        /// <summary>
        /// Waits for a condition to become true with timeout and polling.
        /// </summary>
        /// <param name="condition">Function that returns true when condition is met</param>
        /// <param name="timeoutSeconds">Timeout in seconds (default 10)</param>
        /// <param name="failureMessage">Message to show if condition not met within timeout</param>
        /// <param name="pollingIntervalMs">Polling interval in milliseconds (default 50)</param>
        public static async Task WaitForConditionAsync(
            Func<bool> condition,
            int timeoutSeconds = 10,
            string failureMessage = "Condition was not met within timeout",
            int pollingIntervalMs = 50)
        {
            var timeout = TimeSpan.FromSeconds(timeoutSeconds);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (!condition() && stopwatch.Elapsed < timeout)
            {
                await Task.Delay(pollingIntervalMs);
            }

            Assert.True(condition(), failureMessage);
        }

        /// <summary>
        /// Waits for a value to equal the expected value with timeout and polling.
        /// </summary>
        public static async Task WaitForValueAsync<T>(
            Func<T> getValue,
            T expectedValue,
            int timeoutSeconds = 10,
            string? failureMessage = null,
            int pollingIntervalMs = 50) where T : IEquatable<T>
        {
            var timeout = TimeSpan.FromSeconds(timeoutSeconds);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (!getValue().Equals(expectedValue) && stopwatch.Elapsed < timeout)
            {
                await Task.Delay(pollingIntervalMs);
            }

            var actualValue = getValue();
            var message = failureMessage ?? $"Expected value {expectedValue} but got {actualValue} after {timeout.TotalSeconds}s";
            Assert.Equal(expectedValue, actualValue);
        }

        /// <summary>
        /// Waits for a count to reach the expected value with timeout and polling.
        /// Useful for waiting for disposal counts, response counts, etc.
        /// </summary>
        public static async Task WaitForCountAsync(
            Func<int> getCount,
            int expectedCount,
            int timeoutSeconds = 10,
            string? failureMessage = null,
            int pollingIntervalMs = 50)
        {
            var timeout = TimeSpan.FromSeconds(timeoutSeconds);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (getCount() < expectedCount && stopwatch.Elapsed < timeout)
            {
                await Task.Delay(pollingIntervalMs);
            }

            var actualCount = getCount();
            var message = failureMessage ?? $"Expected count {expectedCount} but got {actualCount} after {timeout.TotalSeconds}s";
            Assert.Equal(expectedCount, actualCount);
        }

        /// <summary>
        /// Waits for server to start listening on its assigned port.
        /// Essential for tests using port 0 (OS-assigned ports).
        /// </summary>
        public static async Task WaitForServerListeningAsync(
            IDicomServer server,
            int timeoutSeconds = 5)
        {
            await WaitForConditionAsync(
                () => server.IsListening,
                timeoutSeconds,
                $"Server on port {server.Port} failed to start listening within {timeoutSeconds}s",
                pollingIntervalMs: 10);
        }
    }
}
