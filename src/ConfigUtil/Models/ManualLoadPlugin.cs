using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigUtil.Models
{
    public class ManualLoadPlugin
    {
        public string Name { get; set; }

        public static ManualLoadPlugin ReadFrom(string filePath)
        {
            string fileNameWithoutExtension
                = Path.GetFileNameWithoutExtension(
                    Path.GetFileNameWithoutExtension(filePath));

            return new ManualLoadPlugin()
            {
                Name = fileNameWithoutExtension,
            };
        }

        public static IEnumerable<ManualLoadPlugin> GetAvailablePlugins(string serverPath)
        {
            var pluginsPath = Path.Combine(serverPath, "osvr-plugins-0");
            return from file in Directory.GetFiles(pluginsPath)
                   where file.Contains("manualload")
                   select ReadFrom(file);
        }
    }
}
