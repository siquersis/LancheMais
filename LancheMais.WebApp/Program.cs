//using LancheMais.WebApp;
//using LancheMais.WebApp.Context;
//using LancheMais.WebApp.Context.Interfaces;
//using LancheMais.WebApp.Context.Repositories;
//using LancheMais.WebApp.Interfaces;
//using LancheMais.WebApp.Models;
//using LancheMais.WebApp.Repositories;
//using LancheMais.WebApp.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder(args);
//var startup = new Startup(builder.Configuration);
//startup.ConfigureServices(builder.Services);
//// Add services to the container.;
//builder.Services.AddControllersWithViews();
//builder.Services.AddMemoryCache();
//builder.Services.AddSession();

//builder.Services.AddDbContext<AppDbContext>(
//    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//    );

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//                .AddEntityFrameworkStores<AppDbContext>()
//                .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(opt =>
//{
//    opt.Password.RequireDigit = true;
//    opt.Password.RequireLowercase = true;
//    opt.Password.RequireNonAlphanumeric = true;
//    opt.Password.RequireUppercase = true;
//    opt.Password.RequiredLength = 6;
//    opt.Password.RequiredUniqueChars = 1;
//});

//builder.Services.AddTransient<ILancheRepository, LancheRepository>();
//builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
//builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
//builder.Services.AddTransient<ISeedUserInitial, SeedUserInitial>();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));


//builder.Services.AddMvcCore();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<AppDbContext>();
//    context.Database.EnsureCreated();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//startup.Configure(app, seedUserInitial: ISeedUserInitial);

//app.UseSession();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "categoriaFiltro",
//    pattern: "Lanche/{action}/{categoria?}",
//    defaults: new { Controller = "Lanche", Action = "List" });

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
using LancheMais.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}