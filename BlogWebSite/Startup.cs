using BlogWebSite.Configuration;
using BlogWebSite.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace BlogWebSite
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<BlogManagementContext>(options =>
            //    options.u(ConnectionString));

            services.AddDbContext<BlogManagementContext>(options => options.UseInMemoryDatabase(databaseName: "BlogManagement"));
          
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name :"v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));
            services.AddAuthentication(options =>
            {   
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(jwt =>
              {
                  var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);

                  jwt.SaveToken = true;
                  jwt.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      RequireExpirationTime = false,
                      ValidateLifetime = true
                  };
              });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/error");
            
            app.UseHttpsRedirection();
            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "My API");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
