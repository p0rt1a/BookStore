using AutoMapper;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.DbOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 0;

            //act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_Book_ShouldBeReturn()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 1;

            //act
            var book = FluentActions.Invoking(() => query.Handle()).Invoke();

            //arrange
            book.Title.Should().NotBeNullOrEmpty();
            book.Genre.Should().NotBeNullOrEmpty();
            book.Author.Should().NotBeNullOrEmpty();
            book.PageCount.Should().BeGreaterThan(0);
            book.PublishDate.Should().NotBeNullOrEmpty();
        }
    }
}
