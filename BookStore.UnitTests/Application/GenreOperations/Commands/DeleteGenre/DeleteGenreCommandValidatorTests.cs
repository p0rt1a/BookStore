using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
