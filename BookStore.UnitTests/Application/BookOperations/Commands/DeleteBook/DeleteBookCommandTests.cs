using BookStore.Application.BookOperations.DeleteBook;
using BookStore.DbOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 0;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrange
            var book = new Book()
            {
                Title = "WhenAlreadyExistBookIdIsGiven_Book_ShouldBeDeleted",
                GenreId = 1,
                AuthorId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 4;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var deletedBook = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            deletedBook.Should().BeNull();
        }
    }
}
