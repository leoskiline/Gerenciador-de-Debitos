using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos
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
            #region Servico para Cookie Authorization
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name = "CookieAuth";
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Autorizacao", new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                  .RequireAuthenticatedUser().Build());
            });
            #endregion

            #region Serviço para obter o usuário autenticado
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UsuarioAutenticado>();
            #endregion
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                    name: "default",
                    pattern: "{controller=Debito}/{action=Index}");
            });
        }
    }
}
