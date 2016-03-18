using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConfigUtil.Common
{
    public static class ConfigExtensions
    {
        public static string GetOSVRServerDirectory(this IConfiguration config)
        {
            return config.Get<string>("OSVR_SERVER_ROOT", null);
        }
    }
}
