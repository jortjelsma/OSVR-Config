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

namespace OSVR.Config.Models
{
    public static class DirectMode
    {
        private static string GetArguments(string threeLetterVendorPNPID)
        {
            string ret = "--no-wait";
            if(!string.IsNullOrWhiteSpace(threeLetterVendorPNPID))
            {
                ret = $"{ret} {threeLetterVendorPNPID}";
            }
            return ret;
        }

        public static void Disable(string serverPath, string threeLetterVendorPNPID = null)
        {
            var disableDirectModeFileName = OSExeUtil.PlatformSpecificExeName("DisableOSVRDirectMode");
            var disableDirectModePath = System.IO.Path.Combine(serverPath, disableDirectModeFileName);
            Process.Start(disableDirectModePath, GetArguments(threeLetterVendorPNPID));
        }

        public static void Enable(string serverPath, string threeLetterVendorPNPID = null)
        {
            var enableDirectModeFileName = OSExeUtil.PlatformSpecificExeName("EnableOSVRDirectMode");
            var enableDirectModePath = System.IO.Path.Combine(serverPath, enableDirectModeFileName);
            Process.Start(enableDirectModePath, GetArguments(threeLetterVendorPNPID));
        }
    }
}
