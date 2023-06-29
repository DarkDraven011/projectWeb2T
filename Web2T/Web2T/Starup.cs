using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Web2T.Models;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var stringConnectdb = Configuration.GetConnectionString("db2T");
        services.AddDbContext<DbMarketsContext>(option => option.UseSqlServer(stringConnectdb));
        services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

        services.AddControllersWithViews().AddRazorRuntimeCompilation();
        services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}