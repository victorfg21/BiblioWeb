using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BiblioWeb.Data;
using BiblioWeb.Models;
using BiblioWeb.Services;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Memory;
using SixLabors.ImageSharp.Web.Middleware;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Resolvers;
using Microsoft.Extensions.Options;

namespace BiblioWeb
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
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(
                        Configuration.GetConnectionString("BaseBiblioWeb")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddImageSharpCore()
                .SetRequestParser<QueryCollectionRequestParser>()
                .SetBufferManager<PooledBufferManager>()
                .SetCache(provider => new PhysicalFileSystemCache(
                    provider.GetRequiredService<IHostingEnvironment>(),
                    provider.GetRequiredService<IBufferManager>(),
                    provider.GetRequiredService<IOptions<ImageSharpMiddlewareOptions>>())
                {
                    Settings =
                    {
                        [PhysicalFileSystemCache.Folder] = PhysicalFileSystemCache.DefaultCacheFolder,
                        [PhysicalFileSystemCache.CheckSourceChanged] = "true"
                    }
                })
                .SetCacheHash<CacheHash>()
                .SetAsyncKeyLock<AsyncKeyLock>()
                .AddResolver<PhysicalFileSystemResolver>()
                .AddProcessor<ResizeWebProcessor>()
                .AddProcessor<FormatWebProcessor>()
                .AddProcessor<BackgroundColorWebProcessor>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Livros}/{action=Index}/{id?}");
            });
        }
    }
}
