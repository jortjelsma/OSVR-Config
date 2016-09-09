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
using System.Diagnostics;
using System.IO;
using System.Linq;
using ConfigUtil.Common;

namespace ConfigUtil.Models
{
    public static class OSVRServer
    {
        public static void Start(string serverPath)
        {
            var osvrServerExeFileName = OSExeUtil.PlatformSpecificExeName("osvr_server");
            var osvrServerExe = Path.Combine(serverPath, osvrServerExeFileName);
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = osvrServerExe;
            startInfo.WorkingDirectory = serverPath;
            Process.Start(startInfo);
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
    }
}
