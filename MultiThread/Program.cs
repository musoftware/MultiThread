using SeasideResearch.LibCurlNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;

namespace MultiThread
{
    class Program
    {

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(10, 5);
            ThreadPool.SetMaxThreads(10, 5);
            FastRequest.GlobalInit();
            for (int i = 0; i < 1000; i++)
            {
                int tmp = i;
                ThreadPool.QueueUserWorkItem((a) =>
                {
                    try
                    {
                        string access = "EAACEdEose0cBAP7CFo4cGOY3ZBjGZCT14D3M0wuFltzcMbZBkT1qz2EI3AHc2ysuSNZB8agSTXvIYnPfjng7FiNqYLhmseN5kP7owEac3CXsPtsWE56A3GAIVASxuhfHEFSKIaplqjegW9COu9Pksg8BbqZB1ZC4sZCD4hNHUZCQkAZDZD";

                        Stopwatch s = new Stopwatch();
                        s.Start();
                        var res = new FastRequest("https://graph.facebook.com/me?access_token=" + access).Perform();
                        s.Stop();
                        long milli = s.ElapsedMilliseconds;
                        s.Reset();
                        s.Start();
                        var res2 = new WebClient().DownloadString("https://graph.facebook.com/me?access_token=" + access);
                        s.Stop();
                        Console.WriteLine(tmp + " via .NET: "  + s.ElapsedMilliseconds + " via CURL: " + milli);
                    }
                    catch (Exception)
                    {

                    }
                }, null);
            }

            Console.ReadKey();
      
            FastRequest.GlobalCleanup();

        }
        public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb, Object extraData)
        {
            string data = System.Text.Encoding.UTF8.GetString(buf);
            Console.WriteLine(data);
            return size * nmemb;
        }
    }
}
