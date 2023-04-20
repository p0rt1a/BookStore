using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var genre = new Genre()
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            //act & assert
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn",
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenValidInputGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = "Noval"
            };

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == command.Model.Name);
            genre.Name.Should().Be(command.Model.Name);
            genre.IsActive.Should().BeTrue();
        }
    }
}
