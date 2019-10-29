using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Self.Drive.Web.Models;
using Self.Drive.Web.Services;

namespace Self.Drive.Web
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DocumentsDatabaseSettings>(Configuration.GetSection(nameof(DocumentsDatabaseSettings)));
            services.AddSingleton<IDocumentsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DocumentsDatabaseSettings>>().Value);

            services.AddSingleton<DocumentService>();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.UseMemberCasing();
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200/");
                    });
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(MyAllowSpecificOrigins);
        }
    }
}
