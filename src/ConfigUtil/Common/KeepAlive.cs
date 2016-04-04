using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigUtil.Common
{
    public static class KeepAlive
    {
        static Thread thread;
        static int pingNumber = 0;
        static object pingNumberLock = new object();

        public static void StartThread()
        {
            if(thread == null)
            {
                thread = new Thread(() =>
                {
                    bool finished = false;
                    while (!finished)
                    {
                        int lastPingNumber = -1;
                        lock (pingNumberLock)
                        {
                            lastPingNumber = pingNumber;
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        lock (pingNumberLock)
                        {
                            if (pingNumber > 0 && lastPingNumber == pingNumber)
                            {
                                finished = true;
                                Environment.FailFast("Not really a failure. Just timed out and CoreCLR doesn't have a nice way to exit gracefully yet.");
                            }
                        }
                    }
                });
                thread.Start();
            }
        }

        public static void Ping()
        {
            lock (pingNumberLock)
            {
                pingNumber++;
            }
        }
    }
}
