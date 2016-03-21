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
using ConfigUtil.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace ConfigUtil.Models
{
    public class OSVRConfig
    {
        public JObject Body { get; set; }
        public IEnumerable<OSVRInclude> Includes { get; set; }

        public static OSVRConfig GetCurrent(IConfiguration config)
        {
            var ret = new OSVRConfig();
            var serverRoot = config.GetOSVRServerDirectory();
            var configPath = Path.Combine(serverRoot, "osvr_server_config.json");
            var includes = new List<OSVRInclude>();
            ret.Includes = includes;

            using (var configReader = File.OpenText(configPath))
            {
                ret.Body = (JObject)JObject.ReadFrom(new JsonTextReader(configReader));

                // Display
                var displayInclude = OSVRInclude.Parse(ret.Body, "display", serverRoot);
                if(displayInclude != null)
                {
                    includes.Add(displayInclude);
                }

                // RenderManager
                var renderManagerInclude = OSVRInclude.Parse(ret.Body, "renderManagerConfig", serverRoot);
                if(renderManagerInclude != null)
                {
                    includes.Add(renderManagerInclude);
                }
            }
            return ret;
        }
    }
}
