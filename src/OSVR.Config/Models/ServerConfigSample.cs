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
using System.Threading.Tasks;

namespace OSVR.Config.Models
{
    public class ServerConfigSample
    {
        public string FileName { get; set; }
        public OSVRConfig Body { get; set; }

        public static IEnumerable<ServerConfigSample> GetAvailableSamples(string serverPath)
        {
            var displaysPath = System.IO.Path.Combine(serverPath, "sample-configs");

            var displayFiles =
                from displayFile in System.IO.Directory.EnumerateFiles(displaysPath)
                where displayFile.Contains("osvr_server_config")
                select new ServerConfigSample()
                {
                    FileName = System.IO.Path.GetFileName(displayFile),
                    Body = OSVRConfig.Read(displayFile, serverPath),
                };

            // These will be filtered out for the client, but log something
            // to let us know of badly formed sample configs.
            foreach(var errorDisplayFile in
                from displayFile in displayFiles
                where displayFile.Body == null && displayFile.FileName != null
                select displayFile)
            {
                Console.WriteLine("Could not parse sample {0}", errorDisplayFile.FileName);
            }

            // filter the sample configs to only those that parse correctly.
            return from displayFile in displayFiles
                   where displayFile.Body != null && displayFile.FileName != null
                   select displayFile;
        }
    }
}
