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

namespace ConfigUtil.Models
{
    public static class DirectMode
    {
        public static void Disable(string serverPath)
        {
            var disableDirectModePath = System.IO.Path.Combine(serverPath, "DisableOSVRDirectMode.exe");
            var disableDirectModePathAMD = System.IO.Path.Combine(serverPath, "DisableOSVRDirectModeAMD.exe");
            Process.Start(disableDirectModePath);
            Process.Start(disableDirectModePathAMD);
        }

        public static void Enable(string serverPath)
        {
            var enableDirectModePath = System.IO.Path.Combine(serverPath, "EnableOSVRDirectMode.exe");
            var enableDirectModePathAMD = System.IO.Path.Combine(serverPath, "EnableOSVRDirectModeAMD.exe");
            Process.Start(enableDirectModePath);
            Process.Start(enableDirectModePathAMD);
        }
    }
}
