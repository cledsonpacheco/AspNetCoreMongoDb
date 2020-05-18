using Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Services.Catalog;

namespace AspNetMongoDB
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeAppSettings();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddJsonOptions(x =>
               {
                   x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               });

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient(typeof(IRepository<>), typeof(MongoDBRepository<>));            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            app.UseMvc();
        }


        private void InitializeAppSettings()
        {
            var mongoSection = Configuration.GetSection("MongoDB");
            var mongoHost = mongoSection.GetValue<string>("DBHost");
            var mongoDbName = mongoSection.GetValue<string>("DBName");

            AppSettings.Settings.ConnectionStrings.DbHost = mongoHost;
            AppSettings.Settings.ConnectionStrings.DbName = mongoDbName;
        }
    }
}
