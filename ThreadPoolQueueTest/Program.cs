using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadPoolQueueTest
{
    class Program
    {
        private static ThreadPoolQueue<string> threadpool = new ThreadPoolQueue<string>();
        private static string[] groups = new string[] { "group-0", "group-1", "group-2", "group-3", "group-4"};
        private static Dictionary<string, List<int>> results = new Dictionary<string, List<int>>();
        private static object mutex = new object();
        static void Main(string[] args)
        {
            threadpool.DefaultGroup = "group-0";
            for (int i = 0; i < 100; ++i)
            {
                threadpool.Queue(groups[i % 5], ThreadProcessing, i);
                Thread.Sleep(500);
            }
            System.Console.ReadLine();
            foreach (var result in results.Keys.ToList())
            {
                System.Console.WriteLine("Group {0}", result);
                foreach (var record in results[result])
                {
                    System.Console.Write("Item={0}\t", record);
                }
                System.Console.WriteLine();
            }
            System.Console.ReadKey();

        }

        private static void ThreadProcessing(object state)
        {
            int record = (int)state;
            Thread.Sleep(500);
            lock(mutex)
            {
                List<int> recordList = new List<int>();
                if (!results.TryGetValue(groups[record % 5], out recordList))
                {
                    results.Add(groups[record % 5], new List<int>());
                }
                results[groups[record % 5]].Add(record);
            }
        }
    }
}
