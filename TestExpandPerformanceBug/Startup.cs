using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using TestExpandPerformanceBug.Models;

namespace TestExpandPerformanceBug
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
            services.AddMvc(mvcOpts =>
            {
                mvcOpts.EnableEndpointRouting = false;
            });
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);

            builder.EntitySet<DummyObject1>("DummyObject");

            builder.EntitySet<DummyObject2>("DummyObject2");
            builder.EntitySet<DummyObject3>("DummyObjectOther");

            // Add dummy functions to make the model arbitrarily large
            // in order to test query performance on large models
            // comment this for-loop out if you want to test with a small model
            for (int i = 0; i < 20000; i++)
            {
                var f = builder.Function($"function{i}");
                f.Parameter(typeof(string), "p1");
                f.Returns(typeof(decimal));
            }

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter();
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());
            });
        }
    }
}
