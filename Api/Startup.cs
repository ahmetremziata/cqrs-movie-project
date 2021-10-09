using System.Collections.Generic;
using System.Net;
using Api.Infrastructures.Extensions;
using Api.Utils;
using Confluent.Kafka;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
using Logic.AppServices.Queries;
using Logic.AppServices.Queries.Handlers;
using Logic.Business.Service.Crud;
using Logic.Business.Service.Kafka;
using Logic.Business.Service.Kafka.Interfaces;
using Logic.Data.DataContexts;
using Logic.Data.Repositories;
using Logic.Data.Repositories.Interfaces;
using Logic.Decorators;
using Logic.Responses;
using Logic.Utils;
using MediatR;
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
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieService, MovieService>();
            /*services.AddTransient<ICommandHandler<EditMovieInfoCommand>>(provider =>
                new AuditLoggingDecorator<EditMovieInfoCommand>
                    (new EditMovieInfoCommandHandler(provider.GetService<MovieDataContext>())));*/
            services.AddTransient<ICommandHandler<InsertCountryInfoCommand>, InsertCountryInfoCommandHandler>();
            services.AddTransient<ICommandHandler<EditMovieInfoCommand>, EditMovieInfoCommandHandler>();
            services.AddTransient<IInsertCommandHandler<InsertMovieInfoCommand>, InsertMovieInfoCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteMovieCommand>>(provider =>
                new AuditLoggingDecorator<DeleteMovieCommand>
                    (new DeleteMovieCommandHandler(provider.GetService<MovieDataContext>(), provider.GetService<IConfiguration>(), provider.GetService<IProducerService>(), provider.GetService<ILogger<DeleteMovieCommandHandler>>())));
            services.AddTransient<ICommandHandler<UpsertPersonToMovieCommand>, UpsertPersonToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<InsertTypeInfoCommand>, InsertTypeInfoCommandHandler>();
            services.AddTransient<ICommandHandler<EditTypeInfoCommand>, EditTypeInfoCommandHandler>();
            services.AddTransient<ICommandHandler<EditCountryInfoCommand>, EditCountryInfoCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteTypeCommand>, DeleteTypeCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteCountryCommand>, DeleteCountryCommandHandler>();
            services.AddTransient<ICommandHandler<UpsertCountryToMovieCommand>, UpsertCountryToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<UpsertTypeToMovieCommand>, UpsertTypeToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<ActivateMovieCommand>, ActivateMovieCommandHandler>();
            services.AddTransient<ICommandHandler<DeactivateMovieCommand>, DeactivateMovieCommandHandler>();
            services.AddTransient<ICommandHandler<SynchronizeMovieCommand>, SynchronizeMovieCommandHandler>();
            services.AddTransient<ICommandHandler<EditPersonInfoCommand>, EditPersonInfoCommandHandler>();
            services.AddTransient<ICommandHandler<DeletePersonCommand>, DeletePersonCommandHandler>();
            services.AddTransient<ICommandHandler<InsertRoleInfoCommand>, InsertRoleInfoCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveActorFromMovieCommand>, RemoveActorFromMovieCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveTypeFromMovieCommand>, RemoveTypeFromMovieCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveCountryFromMovieCommand>, RemoveCountryFromMovieCommandHandler>();
            services.AddTransient<ICommandHandler<InsertActorToMovieCommand>, InsertActorToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<InsertTypeToMovieCommand>, InsertTypeToMovieCommandHandler>();
            services.AddTransient<ICommandHandler<InsertCountryToMovieCommand>, InsertCountryToMovieCommandHandler>();
            services.AddTransient<IInsertCommandHandler<InsertPersonInfoCommand>, InsertPersonInfoCommandHandler>();
            services.AddTransient<IQueryHandler<GetMovieListQuery, List<MovieResponse>>, GetMovieListQueryHandler>();
            //services.AddTransient<IQueryHandler<GetMovieByIdQuery, MovieDetailResponse>, GetMovieByIdQueryHandler>();
            services.AddTransient<IQueryHandler<GetMovieByIdQuery, MovieDetailResponse>, GetMovieByIdWithSeperateDomainModelQueryHandler>();
            services.AddTransient<IQueryHandler<GetMoviePresentationListQuery, MoviePresentationResponse>, GetMoviePresentationListQueryHandler>();
            services.AddTransient<IQueryHandler<GetMoviePresentationQuery, Logic.Indexes.Movie>, GetMoviePresentationQueryHandler>();
            services.AddTransient<IQueryHandler<GetTypeListQuery, List<TypeResponse>>, GetTypeListQueryHandler>();
            services.AddTransient<IQueryHandler<GetTypeByIdQuery, TypeResponse>, GetTypeByIdQueryHandler>();
            services.AddTransient<IQueryHandler<GetCountryListQuery, List<CountryResponse>>, GetCountryListQueryHandler>();
            services.AddTransient<IQueryHandler<GetCountryByIdQuery, CountryResponse>, GetCountryByIdQueryHandler>();
            services.AddTransient<IQueryHandler<GetPersonListQuery, List<PersonResponse>>, GetPersonListQueryHandler>();
            services.AddTransient<IQueryHandler<GetPersonByIdQuery, PersonDetailResponse>, GetPersonByIdQueryHandler>();
            services.AddTransient<IQueryHandler<GetRoleListQuery, List<RoleResponse>>, GetRoleListQueryHandler>();
            services.AddTransient<IQueryHandler<GetMovieListByFilterQuery, List<MovieResponse>>, GetMovieListByFilterQueryHandler>();
            services.AddSingleton<Messages>();
            services.AddTransient<IProducerService, ProducerService>();
            
            services.AddCors(
                o => o.AddPolicy("AllAcceptedPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
            
            services.AddMvc().AddMvcOptions(o =>
            { 
                o.EnableEndpointRouting = false;
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<MovieDataContext>(optionsBuilder =>
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("MovieConnection")),
                ServiceLifetime.Singleton);
            services.AddSwaggerDocumentation("Movie API", "v1.0");

            var connectionString = new ConnectionString(Configuration.GetConnectionString("MovieConnection"));
            services.AddSingleton(connectionString);
            
            var kafkaProducerConfig = new ProducerConfig
            {
                BootstrapServers = Configuration["KafkaBootstrapServers"],
                ClientId = Dns.GetHostName()
            };
            
            services.AddSingleton<ProducerConfig>(kafkaProducerConfig);
            services.AddElasticsearch(Configuration);
            //services.AddHandlers();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllAcceptedPolicy");
            app.UseSwagger();
            app.UseSwaggerDocumentation("Movie API", "v1.0");
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}