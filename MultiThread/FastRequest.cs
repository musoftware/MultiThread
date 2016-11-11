using SeasideResearch.LibCurlNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    class FastRequest
    {
        Easy easy = new Easy();

        public static void GlobalInit()
        {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
        }

        public FastRequest(string url)
        {
          
            Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
            easy.SetOpt(CURLoption.CURLOPT_URL, url);
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
            easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYPEER, false);
   
        }

        public Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb, Object extraData)
        {
            string data = System.Text.Encoding.UTF8.GetString(buf);
            buffer += data;
            return size * nmemb;
        }
        
        private string buffer = string.Empty;

        private bool callComplete = false;


        public string Perform()
        {
            CURLcode data = easy.Perform();
            easy.Dispose();
            return buffer;
        }

        public static void GlobalCleanup()
        {
            Curl.GlobalCleanup();
        }
    }
}
