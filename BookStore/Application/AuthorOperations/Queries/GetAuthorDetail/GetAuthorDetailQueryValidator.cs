using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}
