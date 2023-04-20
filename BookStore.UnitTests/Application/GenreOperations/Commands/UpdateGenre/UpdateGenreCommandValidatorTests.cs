using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 0;
            command.Model = new UpdateGenreModel()
            {
                Name = "WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("N")]
        [InlineData("Nov")]
        public void WhenInvalidGenreNameIsGiven_Validator_ShouldBeReturnError(string genreName)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = genreName
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "Noval"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
