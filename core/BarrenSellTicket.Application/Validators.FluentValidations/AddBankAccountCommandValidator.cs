using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Features.Command.Others;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Validators.FluentValidations
{
    public class AddBankAccountCommandValidator:AbstractValidator<AddBankAccountCommand>
    {
        public AddBankAccountCommandValidator()
        {
            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("Account name is required.")
                .MaximumLength(100).WithMessage("Account name must be less than 100 characters.")
                .CheckNull();

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required")
                .MaximumLength(100).WithMessage("Bank name must be less than 100 characters.")
                .CheckNull();

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.")
                .GreaterThan(0).WithMessage("Account number must be a positive number.");

            RuleFor(x => x.SwiftCode)
                .NotEmpty().WithMessage("Swift code is required.")
                .Matches("^[A-Z0-9]{8}$|^[A-Z0-9]{11}$").WithMessage("Invalid Swift code format");

            RuleFor(x => x.Iban)
                .NotEmpty().WithMessage("IBAN is required.")
                .Matches("[A-Z0-9]+$").WithMessage("Invalid IBAN format.")
                .Length(15, 34).WithMessage("IBAN must be between 15 and 34 characters.");

        }
    }
}
