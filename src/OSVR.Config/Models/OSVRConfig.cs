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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSVR.Config.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OSVR.Config.Models
{
    public class OSVRConfig
    {
        public JObject Body { get; set; }
        public IEnumerable<OSVRInclude> Includes { get; set; }

        public static OSVRConfig Read(string filePath, string serverRoot)
        {
            var ret = new OSVRConfig();
            var includes = new List<OSVRInclude>();
            ret.Includes = includes;

            using (var configReader = File.OpenText(filePath))
            using (var jr = new JsonTextReader(configReader))
            {
                JToken token = JObject.ReadFrom(jr);
                if(!(token is JObject))
                {
                    return null;
                }
                ret.Body = (JObject)token;

                // Display
                var displayInclude = OSVRInclude.Parse(ret.Body, "display", serverRoot);
                if (displayInclude != null)
                {
                    includes.Add(displayInclude);
                }

                // RenderManager
                var renderManagerInclude = OSVRInclude.Parse(ret.Body, "renderManagerConfig", serverRoot);
                if (renderManagerInclude != null)
                {
                    includes.Add(renderManagerInclude);
                }
            }
            return ret;
        }

        public static OSVRConfig GetCurrent(IConfiguration config, string serverRoot)
        {
            string configDirectory = OSVRDirectories.GetUserConfigDirectory(serverRoot, true);
            string configFileName = "osvr_server_config.json";
            string userFolderConfigPath = Path.Combine(configDirectory, configFileName);
            string globalConfigPath = Path.Combine(serverRoot, configFileName);
            string configPath = null;
            if (File.Exists(userFolderConfigPath))
            {
                configPath = userFolderConfigPath;
            }
            else if(File.Exists(globalConfigPath))
            {
                configPath = globalConfigPath;
            }
            else
            {
                return null;
            }
            return OSVRConfig.Read(configPath, serverRoot);
        }

        public static void SetCurrent(OSVRConfig value, string serverRoot)
        {
            string configDirectory = OSVRDirectories.GetUserConfigDirectory(serverRoot, true);
            var configPath = Path.Combine(configDirectory, "osvr_server_config.json");

            // ignoring includes.
            var mainConfigBody = value.Body;
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = System.IO.File.CreateText(configPath))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                serializer.Serialize(jw, mainConfigBody);
            }
        }
    }
}
