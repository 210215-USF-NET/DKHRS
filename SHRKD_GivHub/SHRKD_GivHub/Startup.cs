using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GivHubDL;
using GivHubBL;

namespace SHRKD_GivHub
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
            services.AddDbContext<GHDBContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("GHDB")));

            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // set up cookies
                options.Cookie.Name = ".GHDB.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            // TODO: limit origins to Angular Domain and APIs
                            builder.WithOrigins("*")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        }
                        );
                }
                );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SHRKD_GivHub", Version = "v1" });
            });
            services.AddScoped<ILocationRepo, LocationRepoDB>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<ICharityRepo, CharityRepoDB>();
            services.AddScoped<ICharityBL, CharityBL>();
            services.AddScoped<IDonationRepo, DonationRepoDB>();
            services.AddScoped<IDonationBL, DonationBL>();
            services.AddScoped<ISubscriptionRepo, SubscriptionRepoDB>();
            services.AddScoped<ISubscriptionBL, SubscriptionBL>();
            services.AddScoped<ISearchHistoryRepo, SearchHistoryRepoDB>();
            services.AddScoped<ISearchHistoryBL, SearchHistoryBL>();
            services.AddScoped<IFollowRepo, FollowRepoDB>();
            services.AddScoped<IFollowBL, FollowBL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SHRKD_GivHub v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
