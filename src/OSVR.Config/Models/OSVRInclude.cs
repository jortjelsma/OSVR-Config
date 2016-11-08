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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace OSVR.Config.Models
{
    public class OSVRInclude
    {
        public string RelativePath { get; set; }
        public JObject Body { get; set; }

        public static OSVRInclude Parse(JObject json, string fieldName, string serverRoot)
        {
            OSVRInclude ret = null;
            JToken token = null;
            if (json.TryGetValue(fieldName, out token) && token.Type == JTokenType.String)
            {
                string relativePath = token.Value<string>();
                ret = new OSVRInclude();
                ret.RelativePath = relativePath;
                try {
                    using (var includeReader = File.OpenText(Path.Combine(serverRoot, relativePath)))
                    {
                        ret.Body = (JObject)JObject.ReadFrom(new JsonTextReader(includeReader));
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    ret = null;
                }
            }
            return ret;
        }
    }
}
