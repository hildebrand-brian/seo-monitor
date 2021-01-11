using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SeoMonitor.SearchRankings;

namespace SeoMonitor
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    const string FRONTEND_CORS_POLICY_NAME = "frontend";

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient();
        services.AddMvc();
        services.AddMediatR(typeof(Startup));

        services.AddCors(
          options => 
            {
              options.AddPolicy(name: FRONTEND_CORS_POLICY_NAME,
              builder => 
              {
                builder.WithOrigins("http://localhost:8080", "https://localhost:80801");
              });
            }
        );

        services.AddScoped<IGoogleSearchService, GoogleSearchService>();
        services.AddScoped<IGoogleResultMatcher, GoogleResultMatcher>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
      }
      
      app.UseCors(FRONTEND_CORS_POLICY_NAME);
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
