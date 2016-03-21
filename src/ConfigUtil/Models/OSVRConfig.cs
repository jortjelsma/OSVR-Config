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
