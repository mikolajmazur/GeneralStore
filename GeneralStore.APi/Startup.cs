using System;
using System.Reflection;
using AutoMapper;
using GeneralStore.Api.Config;
using GeneralStore.Api.Context;
using GeneralStore.Api.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GeneralStore.APi
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
            services.AddControllers();
            services.AddDataAccessService(Configuration.GetConnectionString("DefaultConnection"));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<StoreContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            } ); 

            services.AddSwagger();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(Startup));
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtConfig>();
            services.AddAuth(jwtSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(serviceProvider);
            CreateTestUsers(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            string[] expectedRoles = { "admin", "employee", "customer" };
            
            foreach(var role in expectedRoles)
            {
                var roleExists = roleManager.RoleExistsAsync(role).Result;
                if (!roleExists)
                {
                    var roleResult = roleManager.CreateAsync(new Role { Name = role });
                    roleResult.Wait();
                }
            }
        }

        private void CreateTestUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var adminEmail = "admin@admin.com";
            var adminPassword = "veryadmin";

            var admin = userManager.FindByEmailAsync(adminEmail).Result;
            if (admin is null)
            {
                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                var adminCreate = userManager.CreateAsync(user, adminPassword);
                adminCreate.Wait();

                if (adminCreate.Result.Succeeded)
                {
                    var roleAddTask = userManager.AddToRoleAsync(user, "admin");
                    roleAddTask.Wait();
                }
            }
        }
    }
}
