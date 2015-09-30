// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Threading
{
    using System.Linq;
    using System.Threading;

    using Xunit;

    public class ThreadPoolQueueTest
    {
        [Fact]
        public void Queue_OrderOfExecutionForSameKey_ShouldBeFifo()
        {
            string[] expected = { "", "", "" }, actual = { "", "", "" };
            bool[] finished = { false, false, false };
            var handle = new ManualResetEventSlim(false);
            var pool = new ThreadPoolQueue<int>(int.MinValue);

            for (var i = 0; i < 99; ++i)
            {
                var group = i % 3;
                expected[group] += string.Format("B{0}E{0}", i);
                pool.Queue(
                    group,
                    state =>
                        {
                            actual[group] += string.Format("B{0}", state);
                            Thread.Sleep(1);
                            actual[group] += string.Format("E{0}", state);
                            Thread.Sleep(1);
                            if ((int)state > 96)
                            {
                                finished[group] = true;
                                if (finished.All(f => f)) handle.Set();
                            }
                        },
                    i);
            }
            handle.Wait(1000);

            Assert.Equal(expected, actual);
        }
    }
}