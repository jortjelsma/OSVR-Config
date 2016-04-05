/// OSVR-Config
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>
/// 
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
            // Disabled temporarily because Environment.FailFast pops up a "Dnx has shut down unexpectedly" dialog.
            // The latest CoreFx code has an Environment.Exit, which would exit more cleanly, possibly without the dialog,
            // but that fix isn't in the latest rc1-final version of coreclr. rc2 is coming soon, but until then, we'll
            // just stay open until they close the console window.
            //if(thread == null)
            //{
            //    thread = new Thread(() =>
            //    {
            //        bool finished = false;
            //        while (!finished)
            //        {
            //            int lastPingNumber = -1;
            //            lock (pingNumberLock)
            //            {
            //                lastPingNumber = pingNumber;
            //            }
            //            Thread.Sleep(TimeSpan.FromSeconds(5));
            //            lock (pingNumberLock)
            //            {
            //                if (pingNumber > 0 && lastPingNumber == pingNumber)
            //                {
            //                    finished = true;
            //                    Environment.FailFast("Not really a failure. Just timed out and CoreCLR doesn't have a nice way to exit gracefully yet.");
            //                    Console.WriteLine("OSVR_Config_backend_kill_signal");
            //                }
            //            }
            //        }
            //    });
            //    thread.Start();
            //}
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
