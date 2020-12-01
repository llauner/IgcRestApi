using AutoMapper;
using IgcRestApi.DataConversion;
using IgcRestApi.Extensions;
using IgcRestApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IgcRestApi
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
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup).Assembly);  // Registering and Initializing AutoMapper

            // ----- Register dependencies -----
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddTransient<IDataConverter, AutoMapperDataConverter>();
            services.AddTransient<IFirestoreService, FirestoreService>();
            services.AddTransient<IFtpService, FtpService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IIgcReaderService, IgcReaderService>();
            services.AddTransient<IAggregatorService, AggregatorService>();
            services.AddTransient<INetcoupeService, NetcoupeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler();    // Use Extensions/ExceptionMiddleWareExtensions.cs

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
