using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ThreadsCount
    {
        public List<Thread> ts=new List<Thread>();
        private object obj = new object();
    //    HttpWebResponse res;
        public ThreadsCount(long count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(RequestUrl);
                thread.Name = "线程" + (i + 1);
                ts.Add(thread);
            }
        }
        public void ThreadStart()
        {
            //while(true)
           // {
                for (int i = 0; i < ts.Count; i++)
                {
                  //  if (ts[i].ThreadState == ThreadState.Unstarted||ts[i].ThreadState==ThreadState.Stopped)
                    ts[i].Start();
                }
           // }
            
        }
        private void RequestUrl()
        {
            lock (obj)
            {
                HttpWebRequest req = WebRequest.Create("http://www.nftz168.com/") as HttpWebRequest;
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                Stream sr = res.GetResponseStream();
                StreamReader reader = new StreamReader(sr, Encoding.GetEncoding("UTF-8"));
                string str = reader.ReadToEnd();
                //reader.Close();
                //res.Close();
                req = null;
                res = null;
                sr = null;
            
                reader.Close();
                reader = null;
               // sr.Close();
                sr = null;
                Console.WriteLine(str);
                str = null;
                Console.WriteLine(Thread.CurrentThread.Name);
                GC.Collect();
            }
        }
    }
}
