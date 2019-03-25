using AutoMapper;
using BackEnd.Movie.AzureBlob;
using BackEnd.Movie.DataReadRepository;
using BackEnd.Movie.DataWriteRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Movie
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);   
            services.AddScoped<IMovieDataReadRepository, MovieDataReadRepository>();           
            services.AddAutoMapper(configure =>
            {
                configure.AddProfile(new MovieMapperProfile());
            });
            services.AddSingleton<IAzureBlobStorage>(factory =>
            {
                return new AzureBlobStorage(new AzureBlobSetings(
                    storageAccount: Configuration["MovieBlobStorageAccount"],
                    storageKey: Configuration["MovieBlobStorageKey"],
                    containerName: Configuration["MovieBlobContainerName"],
                    developmentConnectionString: Configuration["DevelopmentConnectionString"]));
            });
            services.AddSingleton<IMovieDataWriteRepository, MovieDataWriteRepository>();
            services.AddSingleton<IMovieDataProvider, MovieDataProvider>();
            services.AddSingleton<MovieDataUpdateService>();              
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, MovieDataUpdateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
