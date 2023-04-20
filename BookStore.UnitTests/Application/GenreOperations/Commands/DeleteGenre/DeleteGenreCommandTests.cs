using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
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

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü mevcut değil");
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            var genre = new Genre()
            {
                Name = "WhenAlreadyExistGenreIdIsGiven_Genre_ShouldBeDeleted",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 4;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedGenre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            deletedGenre.Should().BeNull();
        }
    }
}
