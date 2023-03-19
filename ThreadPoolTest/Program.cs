using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    internal class Program
    {
        static int totalUrls;
        static void Main(string[] args)
        {

            List<string> urls = new List<string>
            {
                "https://onet.pl",
                "https://pudelek.pl",
                "https://gazeta.pl",
                "https://alx.pl"
            };
            totalUrls = urls.Count;
            ThreadPool.SetMinThreads(5, 5);
            ThreadPool.SetMaxThreads(15, 15);
            foreach (var url in urls)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DownloadData), url);
            }
            
            Console.ReadKey();
        }

        static void DownloadData(object urlObject)
        {
            string url = (string)urlObject;
            Console.WriteLine($"Pobieram dane z {url}");
            WebClient webClient = new WebClient();
            string data = webClient.DownloadString(url);
            Console.WriteLine($"Zakonczono pobieranie {url}, liczba danych: {data.Length}");
            Interlocked.Decrement(ref totalUrls);
        }

        static void CustomMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"ThreadID: {thread.ManagedThreadId}");
            Thread.Sleep(5000);
        }
    }
}
