using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentBusinessApi.Configuration;
using IntelligentBusinessApi.Contracts;
using IntelligentBusinessApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentBusinessApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string defaultCorsPolicyName = "allow_specific_origins";

        public void ConfigureServices(IServiceCollection services)
        {
            var cloudSettings = Configuration.GetSection(CloudConfiguration.SettingName);
            services.Configure<CloudConfiguration>(cloudSettings);

            services.AddTransient<IValetKeyService, ValetKeyService>();

            services.AddCors(options =>
            {
                options.AddPolicy(defaultCorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200",
                                "http://www.contoso.com")
                            .WithHeaders("Content-Type", "Authorization", "Accept")
                            .WithMethods("OPTIONS", "TRACE", "GET", "HEAD", "POST", "PUT", "DELETE", "PATCH");
                    });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(defaultCorsPolicyName);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
