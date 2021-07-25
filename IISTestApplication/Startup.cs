using IISTestApplication.Hubs;
using IISTestApplication.Hubs.Providers;
using IISTestApplication.Repositories;
using IISTestApplication.Repositories.Interfaces;
using IISTestApplication.Services;
using IISTestApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;

namespace IISTestApplication
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
            var mapperConfig = MapperProfile.GetMapperConfiguration();
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:PeopleConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddDbContext<IdentityContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IISTestApplication", Version = "v1" });
            });

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });

            services.AddTransient<IPeopleRepository, PeopleRepository>();
            services.AddTransient<IBsonService, BsonService>();
            services.AddTransient<IRedisService, RedisService>();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddAuthentication()
                .AddJwtBearer(opts =>
                {
                    opts.RequireHttpsMetadata = false;
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwtSecret"])),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };

                    opts.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            var usrmgr = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                            var signinmgr = ctx.HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
                            var username = ctx.Principal.FindFirst(ClaimTypes.Name)?.Value;
                            var idUser = await usrmgr.FindByNameAsync(username);
                            ctx.Principal = await signinmgr.CreateUserPrincipalAsync(idUser);
                        }
                    };
                });

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = $"{Configuration["Redis:Server"]}:{Configuration["Redis:Port"]}";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IISTestApplication v1"));
            app.UseStaticFiles();
            app.UseCors(builder =>
            {
                builder.WithOrigins("https://football.ua");
                builder.AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat", options =>
                {
                    options.ApplicationMaxBufferSize = 64;
                    options.TransportMaxBufferSize = 64;
                    options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1);
                    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                });
                endpoints.MapHub<ControllerHub>("/controller");
                endpoints.MapHub<ChatTokenHub>("/chatToken");
                endpoints.MapHub<ChatUserHub>("/chatUser");
                endpoints.MapHub<ChatGroupHub>("/chatGroup");
            });

            IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
            OrderSeedData.PopulateOrders(app);
        }
    }
}
