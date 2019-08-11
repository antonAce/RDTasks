﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using OrderManagerBLL.Dependencies;
using OrderManagerBLL.Interfaces;
using OrderManagerBLL.Services;

namespace RDWebTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddUnitOfWorkByConnectionString(@"Server=DESKTOP-7VVBMQ6\SQLEXPRESS;Database=EpamWebTask;Trusted_Connection=True;");
            services.AddUnitOfWorkByFileConfig("appsettings.json", "WebTaskConnection");

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IProductService productService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
