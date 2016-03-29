using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConfigUtil.Models;
using Microsoft.Extensions.Configuration;
using ConfigUtil.Common;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class AvailableManualLoadPlugins : Controller
    {
        private readonly IConfiguration config;
        public AvailableManualLoadPlugins(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ManualLoadPlugin> Get()
        {
            var configDir = this.config.GetOSVRServerDirectory();
            var pluginsPath = Path.Combine(configDir, "osvr-plugins-0");
            return from file in Directory.GetFiles(pluginsPath)
                   where file.Contains("manualload")
                   select CreateManualLoadPlugin(file);
        }

        private ManualLoadPlugin CreateManualLoadPlugin(string fileName)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fileName));
            return new ManualLoadPlugin()
            {
                Name = fileNameWithoutExtension,
            };
        }
    }
}
