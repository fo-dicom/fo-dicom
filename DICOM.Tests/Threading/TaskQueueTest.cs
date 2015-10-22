// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Threading
{
    using System.Linq;
    using System.Threading;

    using Xunit;

    [Collection("General")]
    public class TaskQueueTest
    {
        [Fact]
        public void Queue_OrderOfExecutionForSameKey_ShouldBeFifo()
        {
            var locker = new object();

            string[] expected = { "", "", "" }, actual = { "", "", "" };
            bool[] finished = { false, false, false };
            var handle = new ManualResetEventSlim(false);
            var pool = new TaskQueue<int>(int.MinValue);

            for (var i = 0; i < 99; ++i)
            {
                var group = i % 3;
                expected[group] += string.Format("B{0}E{0}", i);
                pool.Queue(
                    group,
                    state =>
                        {
                            lock (locker) actual[group] += string.Format("B{0}", (int)state);
                            Thread.Sleep((int)state % 2 + 1);
                            lock (locker) actual[group] += string.Format("E{0}", (int)state);
                            if ((int)state > 95)
                            {
                                lock (locker)
                                {
                                    finished[group] = true;
                                    if (finished.All(f => f)) handle.Set();
                                }
                            }
                        },
                    i);
            }
            handle.Wait(10000);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Linger_TaskAddedWithinTimeout_ExecutedOnSameThread()
        {
            var pool = new TaskQueue<int> { Linger = 1000 };
            var expected = -1;

            pool.Queue(1, () => { expected = Thread.CurrentThread.ManagedThreadId; });
            Thread.Sleep(100);

            pool.Queue(
                1,
                state =>
                    {
                        var actual = Thread.CurrentThread.ManagedThreadId;
                        Assert.Equal((int)state, actual);
                    },
                expected);
        }

        [Fact]
        public void Linger_TaskAddedAfterTimeout_ExecutedOnDifferentThread()
        {
            var pool = new TaskQueue<int> { Linger = 100 };
            var expected = -1;

            pool.Queue(1, () => { expected = Thread.CurrentThread.ManagedThreadId; });
            Thread.Sleep(200);

            pool.Queue(
                1,
                state =>
                {
                    var actual = Thread.CurrentThread.ManagedThreadId;
                    Assert.NotEqual((int)state, actual);
                },
                expected);
        }
    }
}