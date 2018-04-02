using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService.BLL;
using DomainModel.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask
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
            services.AddMvc();
            services.AddSession();
            RegisterDependencies(services);
            services.AddDbContextPool<DomainDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TestTaskDb"), 
                                                       providerOptions => providerOptions.CommandTimeout(30)));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=UserAnswers}/{id?}");
            });
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped(typeof(IDomainDbContext), typeof(DomainDbContext));
            services.AddScoped(typeof(IUserAnswersBL), typeof(UserAnswersBL));
            services.AddScoped(typeof(ISectorsBL), typeof(SectorsBL));
        }
    }
}
