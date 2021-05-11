//using webApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//using webApi.MIddleware;
//using webApi.Models;
//using webApi.Services;


namespace webApi
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

            var connection = Configuration["DatabaseConnectionString"];
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webApi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Wstrzykiwać od ogólnego do szczegółowego
            //services.AddScoped<IUserService, UserService>();    
            //services.AddScoped<IRestaurantService, RestaurantService>();
            //services.AddScoped<IDiscountCodeService, DiscountCodeService>();
          //  services.AddScoped<IOrderService, OrderService>();
          //  services.AddScoped<IReviewService, ReviewService>();
           // services.AddScoped<IComplaintService, ComplaintService>();
           // services.AddScoped<IAccountService, AccountService>();
           // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           // services.AddDbContext<IO2_RestaurantsContext>(options => options.UseSqlServer(connection)); // database
           // services.AddScoped<ErrorHandlingMiddleware>();
           // services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
           //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                .AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webApi v1"));
            }


            //app.UseMiddleware<ErrorHandlingMiddleware>();

           // app.UseMiddleware<JwtMiddleware>();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
