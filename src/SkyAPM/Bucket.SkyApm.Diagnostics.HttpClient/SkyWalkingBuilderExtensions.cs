﻿/*
 * Licensed to the SkyAPM under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The SkyAPM licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Bucket.SkyApm.Diagnostics.HttpClient
{
    public static class SkyWalkingBuilderExtensions
    {
        public static SkyApmExtensions AddHttpClient(this SkyApmExtensions extensions, string section = "SkyApm:Extend:HttpClient")
        {
            if (extensions == null)
            {
                throw new ArgumentNullException(nameof(extensions));
            }

            var configService = extensions.Services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)configService.ImplementationInstance;
            extensions.Services.Configure<SkyApmHttpClientOption>(configuration.GetSection(section));

            extensions.Services.AddSingleton<ITracingDiagnosticProcessor, HttpClientTracingDiagnosticProcessor>();

            return extensions;
        }
    }
}