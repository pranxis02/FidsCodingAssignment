using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FidsCodingAssignment
{
    public class Startup
    {
        private readonly AppSettings _settings = new AppSettings();

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection("AppSettings").Bind(_settings);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Client.MappingProfile));

            services.AddControllers()
                    .AddJsonOptions(options =>
                     {
                         options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                     });

            Container.Register(Configuration, _settings, services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Synect API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();

            if (env.IsDevelopment() || env.IsEnvironment("WebDevelopment"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(c =>
            {
                c.MapControllers();
            });
        }
    }
}
