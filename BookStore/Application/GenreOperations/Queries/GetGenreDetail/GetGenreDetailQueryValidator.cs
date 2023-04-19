using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}
