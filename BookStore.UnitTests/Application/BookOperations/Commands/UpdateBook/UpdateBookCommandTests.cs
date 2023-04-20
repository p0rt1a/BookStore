using AutoMapper;
using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DbOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenDoesntExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 0;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "WhenValidInputsAreGiven_Book_ShouldBeUpdated",
                GenreId = 1,
                AuthorId = 1
            };
            command.BookId = 1;
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}
