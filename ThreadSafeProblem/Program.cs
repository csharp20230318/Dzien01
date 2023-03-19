using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LockPerformance lockPerformance = new LockPerformance();
            Console.WriteLine($"Lock = {lockPerformance.ClassicLock()}");
            Console.WriteLine($"Semafor = {lockPerformance.Semaphore()}");
            Console.WriteLine($"Monitor = {lockPerformance.MonitorLock()}");
            Console.ReadKey();
        }
    }
}
