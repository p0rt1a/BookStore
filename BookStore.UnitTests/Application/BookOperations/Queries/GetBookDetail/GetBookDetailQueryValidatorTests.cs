using BookStore.Application.BookOperations.GetBookDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 0;

            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            //arrange
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
