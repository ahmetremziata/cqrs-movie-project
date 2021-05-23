using System.Collections.Generic;
using System.Net;
using Api.Infrastructures.Extensions;
using Api.Utils;
using Confluent.Kafka;
using Logic.AppServices;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
using Logic.AppServices.Queries.Handlers;
using Logic.Business.Service.Kafka;
using Logic.Business.Service.Kafka.Interfaces;
using Logic.Data.DataContexts;
using Logic.Data.Repositories;
using Logic.Data.Repositories.Interfaces;
using Logic.Dtos;
using Logic.Responses;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
            services.AddTransient<IQueryHandler<GetMoviePresentationListQuery, MoviePresentationResponse>, GetMoviePresentationListQueryHandler>();
            services.AddSingleton<Messages>();
            services.AddTransient<IProducerService, ProducerService>();
            
            services.AddMvc().AddMvcOptions(o =>
            { 
                o.EnableEndpointRouting = false;
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<MovieDataContext>(optionsBuilder =>
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("MovieConnection")),
                ServiceLifetime.Singleton);
            services.AddSwaggerDocumentation("Movie API", "v1.0");
            
            var kafkaProducerConfig = new ProducerConfig
            {
                BootstrapServers = Configuration["KafkaBootstrapServers"],
                ClientId = Dns.GetHostName()
            };
            
            services.AddSingleton<ProducerConfig>(kafkaProducerConfig);
            services.AddElasticsearch(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerDocumentation("Movie API", "v1.0");
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}