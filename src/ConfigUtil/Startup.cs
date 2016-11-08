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
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using OSVR.Config.Common;
using System.Net;

namespace ConfigUtil
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            KeepAlive.StartThread();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Only allow local IP loopback requests, not remote requests.
            // Just a sanity check. Users hould still have a firewall.

            // NOTE: this will NOT work if the kestral server is running behind a proxy,
            // see ForwardedHeadersMiddleware for a fix. Not applicable yet:
            // https://github.com/aspnet/BasicMiddleware/blob/dev/src/Microsoft.AspNetCore.HttpOverrides/ForwardedHeadersMiddleware.cs#L18
            app.Use(async (context, next) =>
            {
                // based off of this blog post:
                // http://www.strathweb.com/2016/04/request-islocal-in-asp-net-core/
                // written as an OWIN middleware instead of an extension method.
                bool isLocal = false;
                if (!Configuration.GetValue<bool>("Filtering.LocalHostOnly", true))
                {
                    isLocal = true;
                }
                else
                {
                    var connection = context.Connection;
                    if (connection.RemoteIpAddress == null &&
                        connection.LocalIpAddress == null)
                    {
                        isLocal = true;
                    }
                    else if (connection.RemoteIpAddress != null)
                    {
                        isLocal = connection.LocalIpAddress != null
                        ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                        : IPAddress.IsLoopback(connection.RemoteIpAddress);
                    }
                }
                if (isLocal)
                {
                    await next.Invoke();
                }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
