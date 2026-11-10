using FluentValidation;
using LibraryManagement.Application.Contracts.DTOs.Book_DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Contracts.Validators.BookValidations
{
    public class BookUpdateValidator
        : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Writer)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.BookISBN)
                .NotEmpty()
                .Length(13)
                .Matches(@"^\d{13}$")
                .WithMessage("ISBN 13 rakamdan oluşmalıdır.");
        }
    }
}
