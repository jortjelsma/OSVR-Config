using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConfigUtil.Models;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class ServerConfigPresetsController : Controller
    {
        private readonly IConfiguration config;

        public ServerConfigPresetsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ServerConfigPreset> Get()
        {
            var serverPath = config.Get<string>("OSVR_SERVER");
            var displaysPath = System.IO.Path.Combine(serverPath, "bin/sample-configs");

            return from displayFile in System.IO.Directory.EnumerateFiles(displaysPath)
                   where displayFile.Contains("osvr_server_config")
                   select new ServerConfigPreset()
                   {
                       FileName = System.IO.Path.GetFileName(displayFile),
                       Description = GetDescription(displayFile)
                   };
        }

        private static string GetDescription(string displayFile)
        {
            switch(displayFile)
            {
                // @todo add some descriptions?
                // Do we even need descriptions?
                // If so, can we just read the description from the file?
                default:
                    return "";
            }
        }
    }
}
