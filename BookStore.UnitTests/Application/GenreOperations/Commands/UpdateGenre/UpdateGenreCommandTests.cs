using BookStore.Application.GenreOperations.Commands.UpdateGenre;
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

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn()
        {
            var model = new Genre()
            {
                Name = "WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(model);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn"
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenDoesNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeUpdated"
            };
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            genre.Name.Should().Be(command.Model.Name);
        }
    }
}
