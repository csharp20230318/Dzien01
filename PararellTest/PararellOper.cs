using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PararellTest
{
    internal class PararellOper
    {
        Random random = new Random();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void LoopParallelCancel()
        {
            new Thread(() =>
            {
                Thread.Sleep(3_000);
                cancellationTokenSource.Cancel();
            }).Start();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 4;
            parallelOptions.CancellationToken = cancellationTokenSource.Token;

            try
            {
                Parallel.For(0, 10, parallelOptions, i =>
                {
                    long total = LongOperation();
                    Console.WriteLine("{0} - {1}", i, total);
                    Thread.Sleep(2000);
                });
            } catch (OperationCanceledException exc)
            {
                Console.WriteLine(exc.Message);
            }
            sw.Stop();
            Console.WriteLine($"LoopWithPararell - {sw.Elapsed.TotalMilliseconds}");
        }
        public void LoopNoPararell()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                long total = LongOperation();
                Console.WriteLine("{0} - {1}", i, total);
            }
            sw.Stop();
            Console.WriteLine($"LoopNoPararell - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithPararellBreakStop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 4;
            Parallel.For(0, 10, parallelOptions, (i, loopState) =>
            {
                long total = LongOperation();
                if (i >= 5)
                {
                    loopState.Stop();
                }
                Console.WriteLine("{0} - {1}", i, total);
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararell - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithPararell()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 4;
            Parallel.For(0, 10, parallelOptions, i =>
            {
                long total = LongOperation();
                Console.WriteLine("{0} - {1}", i, total);
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararell - {sw.Elapsed.TotalMilliseconds}");
        }


        public void LoopWithPararellForEach()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 4;
            List<int> integersList = Enumerable.Range(0, 10).ToList();
            Parallel.ForEach( integersList, parallelOptions, i =>
            {
                long total = LongOperation();
                Console.WriteLine("{0} - {1}", i, total);
            });
            sw.Stop();
            Console.WriteLine($"LoopWithPararellForEach - {sw.Elapsed.TotalMilliseconds}");
        }

        public void ParallelInvoke()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            Parallel.Invoke( options, 
                () => TestTask(1),
                () => TestTask(2),
                () => TestTask(3),
                () => TestTask(4)
              );
        }

        private long LongOperation()
        {
            long total = 0;
            for (int i = 0; i < 100_000_00; i++)
            {
                total += i;
            }
            return total;
        }

        private void TestTask(int nr)
        {
            Console.WriteLine($"Start zadania [{nr}]");
            Thread.Sleep(random.Next(500, 1200));
            Console.WriteLine($"Koniec zadania [{nr}]");
        }
    }
}
