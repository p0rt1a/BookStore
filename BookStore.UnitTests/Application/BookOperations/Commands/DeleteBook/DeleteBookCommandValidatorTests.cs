using BookStore.Application.BookOperations.DeleteBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 0;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsIsGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
