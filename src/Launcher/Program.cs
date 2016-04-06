using System;
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
using System.Threading;

namespace OSVRConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var appRoot = Path.Combine(Environment.CurrentDirectory, "approot");
            var webCmd = Path.Combine(appRoot, "runtimes\\dnx-coreclr-win-x64.1.0.0-rc1-update1\\bin\\dnx.exe");
            var arguments = "--project \"packages\\ConfigUtil\\1.0.0\\root\" --configuration Release web";
            var startInfo = new ProcessStartInfo(webCmd);
            startInfo.WorkingDirectory = appRoot;
            startInfo.Arguments = arguments;
            var backendProcess = Process.Start(startInfo);
            Thread.Sleep(TimeSpan.FromSeconds(2.0));
            var webProcess = Process.Start("http://localhost:5000");
        }
    }
}
