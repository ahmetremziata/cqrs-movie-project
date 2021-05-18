using System.Collections.Generic;
using Api.Infrastructures.Extensions;
using Api.Utils;
using Logic.AppServices;
using Logic.AppServices.Commands;
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
            services.AddTransient<ICommandHandler<UpsertPersonToMovieCommand>, UpsertPersonToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<UpsertCountryToMovieCommand>, UpsertCountryToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<UpsertTypeToMovieCommand>, UpsertTypeToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<ActivateMovieCommand>, ActivateMovieCommandHandler>();
            services.AddTransient<ICommandHandler<DeactivateMovieCommand>, DeactivateMovieCommandHandler>();
            services.AddTransient<IQueryHandler<GetMovieListQuery, List<MovieDto>>, GetMovieListQueryHandler>();
            services.AddSingleton<Messages>();

            services.AddMvc().AddMvcOptions(o =>
            { 
                o.EnableEndpointRouting = false;
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<MovieDataContext>(optionsBuilder =>
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("MovieConnection")),
                ServiceLifetime.Singleton);
            services.AddSwaggerDocumentation("Movie API", "v1.0");
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerDocumentation("Movie API", "v1.0");
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}