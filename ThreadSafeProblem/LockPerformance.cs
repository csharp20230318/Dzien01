using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeProblem
{
    internal class LockPerformance
    {

        private List<TestClass> dataList = new List<TestClass>(1_000);
        const int maxValue = 1_000_000;
        private SemaphoreSlim slim = new SemaphoreSlim(5);
        private int testValue = 0;

        /// <summary>
        /// Klasyczny lock zmiennych
        /// </summary>
        public double ClassicLock()
        {
            dataList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < maxValue; i++)
            {
                lock (this)
                {
                    TestClass tc = new TestClass();
                    tc.x = 123.45;
                    dataList.Add(tc);
                }
            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public double Semaphore()
        {
            dataList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < maxValue; i++)
            {
                slim.Wait();
                TestClass tc = new TestClass();
                tc.x = 123.45;
                dataList.Add(tc);
                slim.Release();

            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }


        public double MonitorLock()
        {
            dataList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < maxValue; i++)
            {                
                TestClass tc = new TestClass();
                tc.x = 123.45;
                Monitor.Enter(dataList);
                dataList.Add(tc);
                Monitor.Exit(dataList);
            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public void InterlockedTest()
        {
            Interlocked.Increment(ref testValue);
            Interlocked.Add(ref testValue, 2);
            Interlocked.Exchange(ref testValue, -1);
        }
    }
}
