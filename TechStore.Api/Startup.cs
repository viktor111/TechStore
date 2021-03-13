using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Api.Helpers;

namespace TechStore.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<TechStoreDbContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAuthentication();

            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", opt =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audiance,
                        IssuerSigningKey = key
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechStore.Api", Version = "v1" });
            });

            services.AddDataProtection();

            services.AddScoped<Validator>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Cart>, CartRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechStore.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
