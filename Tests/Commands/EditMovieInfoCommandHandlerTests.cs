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
                .Create();
            context.Movies.Add(movie);
            
            EditMovieInfoCommandHandler handler = new EditMovieInfoCommandHandler(context);

            //Act
            var result = await handler.Handle(command);
            
            //Assert
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