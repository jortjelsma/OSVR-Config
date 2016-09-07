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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ConfigUtil.Models;
using ConfigUtil.Common;

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class AvailableDisplaysController : Controller
    {
        private readonly IConfiguration config;
        public AvailableDisplaysController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/availabledisplays
        [HttpGet]
        public IEnumerable<OSVRDisplay> Get()
        {
            string serverPath = this.config.GetOSVRServerDirectory();
            return OSVRDisplay.GetAvailableDisplays(serverPath);
        }
    }
}
