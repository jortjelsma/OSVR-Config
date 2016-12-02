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
using OSVR.Config.Common;
using System.IO;
using System.Diagnostics;

namespace OSVR.Config.Models
{
    public class SampleApp
    {
        internal SampleApp(string name, string fileName, string description)
        {
            if(name == null) { throw new ArgumentNullException(nameof(name)); }
            if(fileName == null) { throw new ArgumentNullException(nameof(fileName)); }
            if(description == null) { throw new ArgumentNullException(nameof(description)); }

            this.Name = name;
            this.FileName = fileName;
            this.Description = description;
        }

        /// <summary>
        /// User friendly name of the app.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// File name of the app without the file extension. Also used
        /// as the id for launching the sample app.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// User friendly description of the app.
        /// </summary>
        public string Description { get; private set; }
    }

    public static class SampleApps
    {
        private static SampleApp[] sampleApps = new SampleApp[]
        {
            new SampleApp(
                name: "RenderManager D3D Example",
                fileName: "RenderManagerD3DExample3D.exe",
                description: "Basic '3D cube room' demo written for Direct3D 11."),
            new SampleApp(
                name: "RenderManager D3D ATW Double-Buffer Example",
                fileName: "RenderManagerD3DATWDoubleBufferExample.exe",
                description: "Basic '3D cube room' demo written for Direct3D 11, which uses alternating render targets optimized for ATW. Simulates rendering lag - will only run 'smoothly' with ATW enabled."),
            new SampleApp(
                name: "RenderManager D3D C-API Example",
                fileName: "RenderManagerD3DCAPIExample.exe",
                description: "Basic '3D cube room' demo written for Direct3D 11 using the C API."),
            new SampleApp(
                name: "RenderManager D3D 2D Test Example",
                fileName: "RenderManagerD3DTest2D.exe",
                description: "Basic 2D demo written in Direct3D 11 to test RenderManager settings without head tracking."),
            new SampleApp(
                name: "RenderManager OpenGL C-API Example",
                fileName: "RenderManagerOpenGLCAPIExample.exe",
                description: "Basic '3D cube room' demo written for OpenGL using the C API."),
            new SampleApp(
                name: "RenderManager OpenGL C-API Example",
                fileName: "RenderManagerOpenGLCAPIExample.exe",
                description: "Basic '3D cube room' demo written for OpenGL using the C API."),
            new SampleApp(
                name: "RenderManager OpenGL Example",
                fileName: "RenderManagerOpenGLExample.exe",
                description: "Basic '3D cube room' demo written for OpenGL."),
            new SampleApp(
                name: "RenderManager OpenGL Core Profile Example",
                fileName: "RenderManagerOpenGLCoreExample.exe",
                description: "Basic '3D cube room' demo written for OpenGL core profile."),
            new SampleApp(
                name: "RenderManager OpenGL High Polygon Test",
                fileName: "RenderManagerOpenGLHighPolyTest.exe",
                description: "Basic '3D cube room' demo written for OpenGL core profile, but renders higher number of polygons to test performance."),
            new SampleApp(
                name: "RenderManager OpenGL Qt5 Example",
                fileName: "RenderManagerOpenGLQt5Example.exe",
                description: "Basic '3D cube room' demo written for OpenGL core profile. Uses Qt5 as the window and graphics context toolkit to test RenderManager integration with externally created OpenGL graphics contexts."),
        };


        public static IEnumerable<SampleApp> GetSampleApps(string serverPath)
        {
            // just return a hard-coded list for now.
            // @todo can we dynamically generate this from a json config file/etc...?
            return sampleApps;
        }

        public static bool RunSampleApp(string fileName, string serverPath)
        {
            if(fileName == null) { throw new ArgumentNullException(nameof(fileName)); }
            if(serverPath == null) { throw new ArgumentNullException(nameof(serverPath)); }

            var sampleApp = GetSampleApps(serverPath).FirstOrDefault(
                app => String.CompareOrdinal(fileName, app?.FileName) == 0);

            if(sampleApp == null)
            {
                return false;
            }

            var platformSpecificFileName = OSExeUtil.PlatformSpecificExeName(
                sampleApp.FileName);

            var fullFileName = Path.Combine(serverPath, platformSpecificFileName);
            OSExeUtil.RunProcessInOwnConsole(fullFileName, serverPath);
            return true;
        }
    }
}
