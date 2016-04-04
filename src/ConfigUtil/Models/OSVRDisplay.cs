using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConfigUtil.Models
{
    public class OSVRDisplay
    {
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public JObject Body { get; set; }

        public static OSVRDisplay ReadFrom(string filePath, string serverRoot)
        {
            OSVRDisplay ret = new OSVRDisplay();
            ret.FileName = Path.GetFileName(filePath);
            ret.RelativePath = filePath.Substring(serverRoot.Length, filePath.Length - serverRoot.Length)
                .Replace('\\', '/');
            using (var configReader = File.OpenText(filePath))
            using (var jr = new JsonTextReader(configReader))
            {
                ret.Body = (JObject)JObject.ReadFrom(jr);
            }
            return ret;
        }

        public static IEnumerable<OSVRDisplay> GetAvailableDisplays(string serverPath)
        {
            string displaysPath = Path.Combine(serverPath, "displays");
            List<OSVRDisplay> ret = new List<OSVRDisplay>();
            foreach (var displayFile in Directory.GetFiles(displaysPath))
            {
                ret.Add(OSVRDisplay.ReadFrom(displayFile, serverPath));
            }
            return ret;
        }
    }
}
