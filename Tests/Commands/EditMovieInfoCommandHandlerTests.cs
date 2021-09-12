using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestProject1.Commands
{
    public class EditMovieInfoCommandHandlerTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public async Task Handler_ThatMovieExists_ReturnsResultFailure()
        {
            //Arrange
            EditMovieInfoCommand command = _fixture.Build<EditMovieInfoCommand>()
                .With(item => item.Id, 1)
                .With(item => item.Name, "UpdatedName")
                .With(item => item.Description, "UpdatedDescription")
                .With(item => item.OriginalName, "UpdatedOriginalName")
                .With(item => item.PosterUrl, "UpdatedPosterUrl")
                .With(item => item.TotalMinute, 2)
                .With(item => item.ConstructionYear, 1901)
                .Create();
            DbContextOptions<MovieDataContext> options = new DbContextOptionsBuilder<MovieDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            MovieDataContext context = new MovieDataContext(options);
            Movie movie = _fixture.Build<Movie>()
                .With(item => item.Id, 1)
                .With(item => item.Name, "Name")
                .With(item => item.Description, "Description")
                .With(item => item.OriginalName, "OriginalName")
                .With(item => item.PosterUrl, "PosterUrl")
                .With(item => item.TotalMinute, 1)
                .With(item => item.ConstructionYear, 1900)
                .With(item => item.IsSynchronized, true)
                .Create();
             await context.Movies.AddAsync(movie);
             await context.SaveChangesAsync();
            
            EditMovieInfoCommandHandler handler = new EditMovieInfoCommandHandler(context);

            //Act
            var result = await handler.Handle(command);
            
            //Assert
            var updatedMovie = await context.Movies.SingleAsync(item => item.Id == 1);
            updatedMovie.Description.Should().Be("UpdatedDescription");
            updatedMovie.Name.Should().Be("UpdatedName");
            updatedMovie.OriginalName.Should().Be("UpdatedOriginalName");
            updatedMovie.PosterUrl.Should().Be("UpdatedPosterUrl");
            updatedMovie.TotalMinute.Should().Be(2);
            updatedMovie.ConstructionYear.Should().Be(1901);
            updatedMovie.IsSynchronized.Should().BeFalse();
            result.IsFailure.Should().BeFalse();
        }
        
        [Test]
        public async Task Handler_ThatAnotherMovieWithSameNameExists_ReturnsResultFailureWithAnotherMovieMessage()
        {
            //Arrange
            EditMovieInfoCommand command = _fixture.Build<EditMovieInfoCommand>()
                .With(item => item.Id, 1)
                .With(item => item.Name, "UpdatedName")
                .With(item => item.Description, "UpdatedDescription")
                .With(item => item.OriginalName, "UpdatedOriginalName")
                .With(item => item.PosterUrl, "UpdatedPosterUrl")
                .With(item => item.TotalMinute, 2)
                .With(item => item.ConstructionYear, 1901)
                .Create();
            DbContextOptions<MovieDataContext> options = new DbContextOptionsBuilder<MovieDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            MovieDataContext context = new MovieDataContext(options);
            Movie movie1 = _fixture.Build<Movie>()
                .With(item => item.Id, 1)
                .With(item => item.Name, "Name")
                .With(item => item.Description, "Description")
                .With(item => item.OriginalName, "OriginalName")
                .With(item => item.PosterUrl, "PosterUrl")
                .With(item => item.TotalMinute, 1)
                .With(item => item.ConstructionYear, 1900)
                .With(item => item.IsSynchronized, true)
                .Create();
             await context.Movies.AddAsync(movie1);
             Movie movie2 = _fixture.Build<Movie>()
                 .With(item => item.Id, 2)
                 .With(item => item.Name, "UpdatedName")
                 .With(item => item.Description, "UpdatedDescription")
                 .With(item => item.OriginalName, "UpdatedOriginalName")
                 .With(item => item.PosterUrl, "UpdatedPosterUrl")
                 .With(item => item.TotalMinute, 1)
                 .With(item => item.ConstructionYear, 1900)
                 .With(item => item.IsSynchronized, true)
                 .Create();
             await context.Movies.AddAsync(movie2);
             await context.SaveChangesAsync();
            
            EditMovieInfoCommandHandler handler = new EditMovieInfoCommandHandler(context);

            //Act
            var result = await handler.Handle(command);
            
            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be($"Movie already found for Name: {command.Name} OriginalName: {command.OriginalName}");
        }
        
        [Test]
        public async Task Handler_ThatMovieNotExist_ReturnsResultFailureWithNotFound()
        {
            //Arrange
            EditMovieInfoCommand command = _fixture.Build<EditMovieInfoCommand>()
                .With(item => item.Id, 1)
                .Create();
            DbContextOptions<MovieDataContext> options = new DbContextOptionsBuilder<MovieDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            MovieDataContext context = new MovieDataContext(options);
            
            EditMovieInfoCommandHandler handler = new EditMovieInfoCommandHandler(context);

            //Act
            var result = await handler.Handle(command);
            
            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be($"No movie found for Id 1");
        }
    }
}