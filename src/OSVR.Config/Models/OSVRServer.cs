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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OSVR.Config.Common;

namespace OSVR.Config.Models
{
    public static class OSVRServer
    {
        public static void Start(string serverPath)
        {
            var osvrServerExeFileName = OSExeUtil.PlatformSpecificExeName("osvr_server");
            var osvrServerExe = Path.Combine(serverPath, osvrServerExeFileName);
            OSExeUtil.RunProcessInOwnConsole(osvrServerExe, serverPath);
        }

        public static void Stop()
        {
            var servers = Process.GetProcessesByName("osvr_server");
            foreach(var server in servers)
            {
                //server.Kill();
                OSExeUtil.SendProcessTerminateSignal(server.Id);
            }
        }

        public static void Restart(string serverPath)
        {
            Stop();
            Start(serverPath);
        }

        public static string ParseServerPath(string environmentValue)
        {
            if (environmentValue != null && environmentValue.Contains(';'))
            {
                var values = environmentValue.Split(';');
                return values.Last();
            }
            return environmentValue ?? "";
        }

        public static string[] RunningServerPaths()
        {
            var servers = Process.GetProcessesByName("osvr_server");
            var ret = new List<string>();
            foreach(var server in servers)
            {
                ret.Add(server.MainModule.FileName);
            }
            return ret.ToArray();
        }
    }
}
