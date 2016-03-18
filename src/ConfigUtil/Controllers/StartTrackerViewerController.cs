using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using ConfigUtil.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class StartTrackerViewerController : Controller
    {
        private readonly IConfiguration config;

        public StartTrackerViewerController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/starttrackerviewer[?paths=path1,path2,...]
        [HttpPost]
        public void Post([FromQuery]IEnumerable<string> paths)
        {
            var osvrServerPath = this.config.GetOSVRServerDirectory();
            var trackerViewerPath = System.IO.Path.Combine(osvrServerPath, "OSVRTrackerView.exe");
            Process.Start(trackerViewerPath, String.Join(" ", paths));
        }
    }
}
