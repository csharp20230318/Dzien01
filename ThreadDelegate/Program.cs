using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDelegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tworzenie wątku z delegatem w celu notyfikacji zdarzenia zakończenia pracy wątku
            ResultCallbackDelegate resultCallback = new ResultCallbackDelegate(ResultCallbackMethod);
            SumHelper sumHelper = new SumHelper(50, resultCallback);
            Thread thread = new Thread(sumHelper.CalculateSum);
            thread.Start();
            Console.WriteLine("Liczę.....");
            thread.Join();
            Console.ReadKey();
        }

        private static void ResultCallbackMethod(int result)
        {
            Console.WriteLine($"Wynik operacji = {result}");
        }
    }
}
