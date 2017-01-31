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
using OSVR.Config.Common;
using System.Threading.Tasks;

namespace OSVR.Config.Models
{
    public static class ResetYaw
    {
        private static string[] GetArguments(string path)
        {
            var ret = new string[] { "--no-wait" };
            if (!string.IsNullOrWhiteSpace(path))
            {
                ret = new string[] { ret[0], path };
            }
            return ret;
        }

        public static async Task CallResetYaw(string serverPath, string path = null)
        {
            var resetYawFileName = OSExeUtil.PlatformSpecificExeName("osvr_reset_yaw");
            var resetYawPath = System.IO.Path.Combine(serverPath, resetYawFileName);
            var process = OSExeUtil.RunProcessInOwnConsole(resetYawPath, serverPath, GetArguments(path));
            var ret = new Task(() => process.WaitForExit());
            ret.Start();
            await ret;
        }
    }
}
