using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class TrackerViewerController : Controller
    {
        // POST api/trackerviewer
        [HttpPost]
        public void Post()
        {
            // @todo launch the tracker viewer
        }
    }
}
