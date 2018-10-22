using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebAPIsTrainingProject.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace WebAPIs_TrainingProject
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

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
        });

            services.AddMvc();

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddDbContext<APIsTrainingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WebAPIsTrainingProjectContext")));

            services.AddSwaggerDocumentation();
            {
                //services.AddSwaggerGen(c =>
                //{
                //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                //    // Swagger 2.+ support
                //    var security = new Dictionary<string, IEnumerable<string>>
                //    {
                //        {"Bearer", new string[] { }},
                //    };
                //    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //    {
                //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //        Name = "Authorization",
                //        In = "header",
                //        Type = "apiKey"
                //    });
                //    c.AddSecurityRequirement(security);
                //});
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwaggerDocumentation();
            {
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
                //    c.DocumentTitle = "Title Documentation";
                //    c.DocExpansion(DocExpansion.None);
                //});
            }
        }
    }
}
