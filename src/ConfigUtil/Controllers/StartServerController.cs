using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using ConfigUtil.Common;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class StartServerController : Controller
    {
        private readonly IConfiguration config;
        public StartServerController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/startserver
        [HttpPost]
        public void Post()
        {
            var osvrServerPath = this.config.GetOSVRServerDirectory();
            var osvrServerExe = System.IO.Path.Combine(osvrServerPath, "osvr_server.exe");
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = osvrServerExe;
            startInfo.WorkingDirectory = osvrServerPath;
            Process.Start(startInfo);
        }
    }
}
