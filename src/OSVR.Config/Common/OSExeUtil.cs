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
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSVR.Config.Common
{
    public static class OSExeUtil
    {
        public static string PlatformSpecificExeName(string normalizedName)
        {
            if (normalizedName == null) { throw new ArgumentNullException("normalizedName"); }

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.ChangeExtension(normalizedName, "exe");
            }
            return normalizedName;
        }

        public static Process RunProcessInOwnConsole(string processFilePath, string workingDirectory, params string[] arguments)
        {
            if (processFilePath == null) { throw new ArgumentNullException("processFileName"); }
            if (workingDirectory == null) { throw new ArgumentNullException("workingDirectory"); }
            if (arguments == null) { throw new ArgumentNullException("arguments"); }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var startInfo = new ProcessStartInfo("cmd.exe");
                var argsStr = arguments.Length == 0 ? "" : $" {String.Join(" ", arguments)}";
                startInfo.Arguments = $"/c start \"{processFilePath}\" /D \"{workingDirectory}\" \"{processFilePath}\"{argsStr}";
                startInfo.WorkingDirectory = workingDirectory;

                return Process.Start(startInfo);
            }
            else
            {
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = processFilePath;
                startInfo.WorkingDirectory = workingDirectory;
                return Process.Start(startInfo);
            }
        }
    }
}
