using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSchoolAPI
{
    public class Startup
    {
        /// <summary>
        /// for application services
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<CustomMiddleWare>();
        }

        /// <summary>
        /// for middleware and http request pipeline
        /// </summary>
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            app.Use(async (context,next) =>
            {
                await context.Response.WriteAsync("Custom MW -- in configure \n");
                await next();
                await context.Response.WriteAsync("Custom MW -- in configure in return flow \n");
            });
            app.UseMiddleware<CustomMiddleWare>();
            //app.Run(async (context)=> {
            //    await context.Response.WriteAsync("from Run() -- in configure \n");
            //});
            app.Map("/sayhi", (app)=> {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Map for sayhi -- in configure \n");
                    await next();
                });
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Map for sayhi -- in configure 2 \n");
                    await next();
                });
                app.Run(async (context) => {
                    await context.Response.WriteAsync("from Run() -- in configure sayhi\n");
                });
            });
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints((endpoints) =>
            {
                endpoints.MapGet("", (context) =>
                {
                    return context.Response.WriteAsync(".Net5 Web API - Home \n");
                });
                endpoints.MapGet("/sayhi", (context) =>
                {
                    return context.Response.WriteAsync("Hi web api users \n");
                });
                endpoints.MapControllers();
            });
        }
    }
}
