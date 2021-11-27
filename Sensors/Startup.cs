using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sensors.Services;
using Sensors.Utils;
using RabbitMqOptions = Sensors.Configuration.RabbitMqOptions;

namespace Sensors
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
            // service with password and username to rabbitMQ
            services.Configure<RabbitMqOptions>(Configuration.GetSection(RabbitMqOptions.RabbitMq));
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Sensors", Version = "v1"}); });

            var sensorService = new SensorService(SensorUtils.CreateSensors());
            services.AddSingleton<ISensorService>(sensorService);

            services.AddMassTransit(config =>
            {
                var rabbitConfiguration = Configuration.GetSection(RabbitMqOptions.RabbitMq).Get<RabbitMqOptions>();
                config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitConfiguration.ServerAddress), hostConfigurator => 
                    { 
                        hostConfigurator.Username(rabbitConfiguration.Username);
                        hostConfigurator.Password(rabbitConfiguration.Password);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sensors v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}