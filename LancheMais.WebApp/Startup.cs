using LancheMais.WebApp.Areas.Admin.Services;
using LancheMais.WebApp.Context;
using LancheMais.WebApp.Context.Interfaces;
using LancheMais.WebApp.Context.Repositories;
using LancheMais.WebApp.Interfaces;
using LancheMais.WebApp.Models;
using LancheMais.WebApp.Repositories;
using LancheMais.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LancheMais.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(opt => opt.AccessDeniedPath = "/Home/AccessDenied");
            services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));

            //services.Configure<IdentityOptions>(options =>
            //{    
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;
            //});

            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ISeedUserInitial, SeedUserInitial>();
            services.AddScoped<RelatorioVendasService>();
            services.AddScoped<GraficoVendasService>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", politica =>
                {
                    politica.RequireRole("Admin");
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

            services.AddControllersWithViews();

            services.AddPaging(opt =>
            {
                opt.ViewName = "Bootstrap4";
                opt.PageParameterName = "pageindex";
            });

            services.AddMemoryCache();

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserInitial seed)
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

            seed.SeedRoles();
            seed.SeedUsers();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "categoriaFiltro",
                   pattern: "Lanche/{action}/{categoria?}",
                   defaults: new { Controller = "Lanche", action = "List" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
