using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApiClientTestApp.Api
{
    public class Startup
    {
        private string _baseFolder;
        private const string ApiName = "My API";
        private const string ApiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Serialize enums as string to be more readable and decrease the risk of incorrect conversion of values.
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiName, Version = ApiVersion });
                
                // Mark decimal properties with the "decimal" format so client contracts can be correctly generated.
                // Credits: https://github.com/domaindrivendev/Swashbuckle/issues/255
                c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
                c.MapType<decimal?>(() => new OpenApiSchema { Type = "number", Format = "decimal" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(_baseFolder, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _baseFolder = env.ContentRootPath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
            });
        }
    }
}
