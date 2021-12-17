using FluentValidation.AspNetCore;
using JurisTempus.Data;
using JurisTempus.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Reflection;

namespace JurisTempus
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<BillingContext>(cfg =>
      {
        cfg.UseSqlServer(Configuration.GetConnectionString("JurisDb"));
      });

      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      // Either/Or: services.AddRazorPages().AddFluentValidation();
      services.AddRazorPages();
      services.AddControllersWithViews()
        .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CaseViewModelValidator>())
        .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
