using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OSVR.Config.Models
{
    public class Plugin
    {
        public string Name { get; set; }
        public bool ManualLoad { get; set; }

        public static Plugin ReadFrom(string filePath)
        {
            string fileNameWithoutExtension
                = Path.GetFileNameWithoutExtension(
                    Path.GetFileNameWithoutExtension(filePath));

            return new Plugin()
            {
                Name = fileNameWithoutExtension,
                ManualLoad = filePath.Contains("manualload"),
            };
        }

        static bool FilterPluginsByName(string pluginFileName, bool withManualPlugins, bool withAutoPlugins)
        {
            bool isManualPlugin = pluginFileName.Contains("manualload");
            return (isManualPlugin && withManualPlugins) || (!isManualPlugin && withAutoPlugins);
        }

        public static IEnumerable<Plugin> GetAvailablePlugins(string serverPath, bool withManualPlugins, bool withAutoPlugins)
        {
            var pluginsPath = Path.Combine(serverPath, "osvr-plugins-0");
            return from file in Directory.GetFiles(pluginsPath)
                   where FilterPluginsByName(file, withManualPlugins, withAutoPlugins)
                   select ReadFrom(file);
        }
    }
}
