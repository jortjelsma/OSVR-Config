using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ConfigUtil.Models
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
                using (var includeReader = File.OpenText(Path.Combine(serverRoot, relativePath)))
                {
                    ret.Body = (JObject)JObject.ReadFrom(new JsonTextReader(includeReader));
                }
            }
            return ret;
        }
    }
}
