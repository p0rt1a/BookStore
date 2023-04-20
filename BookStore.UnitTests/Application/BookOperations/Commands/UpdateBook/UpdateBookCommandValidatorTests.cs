using BookStore.Application.BookOperations.UpdateBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Fact]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 0;
            
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
