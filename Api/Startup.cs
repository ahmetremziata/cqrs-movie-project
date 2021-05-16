using System.Collections.Generic;
using Api.Utils;
using Logic.AppServices;
using Logic.AppServices.Handlers;
using Logic.Data.DataContexts;
using Logic.Data.Repositories;
using Logic.Data.Repositories.Interfaces;
using Logic.Dtos;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ICommandHandler<EditMovieInfoCommand>, EditMovieInfoCommandHandler>();
            services.AddTransient<ICommandHandler<InsertMovieInfoCommand>, InsertMovieInfoCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteMovieCommand>, DeleteMovieCommandHandler>();
            services.AddTransient<IQueryHandler<GetMovieListQuery, List<MovieDto>>, GetMovieListQueryHandler>();
            services.AddSingleton<Messages>();

            services.AddMvc().AddMvcOptions(o =>
            { 
                o.EnableEndpointRouting = false;
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<MovieDataContext>(optionsBuilder =>
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("MovieConnection")),
                ServiceLifetime.Singleton);
            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}