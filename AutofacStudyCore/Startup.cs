using System;
using Autofac;
using AutofacStudyCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutofacStudyCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddControllersAsServices();
            services.AddHttpClient<ITestService1, TestService>(c =>
            {
                c.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());
            });
            services.AddHttpClient<ITestService2, TestService>("named", c =>
            {
                c.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());
            });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterType<TestService>().As<ITestService1>();
            builder.RegisterType<TestService>().As<ITestService2>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}