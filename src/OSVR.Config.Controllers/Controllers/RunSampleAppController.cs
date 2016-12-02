/// OSVR-Config
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>
/// 
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OSVR.Config.Models;
using Microsoft.Extensions.Configuration;
using OSVR.Config.Common;

namespace OSVR.Config.Controllers
{
    [Route("api/[controller]")]
    public class RunSampleAppController : Controller
    {
        private readonly IConfiguration config;

        public RunSampleAppController(IConfiguration config)
        {
            this.config = config;
        }

        // POST: api/runsampleapp?sampleAppFileName=sampleAppFileName
        [HttpPost]
        public IActionResult Post(string sampleAppFileName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var serverPath = this.config.GetOSVRServerDirectory();
            if (!SampleApps.RunSampleApp(sampleAppFileName, serverPath))
            {
                return NotFound();
            }

            return Content("Sample app run successfully!");
        }
    }
}
