using BookStore.Application.BookOperations.GetBookDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
