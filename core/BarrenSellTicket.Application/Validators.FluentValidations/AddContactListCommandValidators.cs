using BarrenSellTicket.Application.Features.Command.Others;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Validators.FluentValidations
{
    public class AddContactListCommandValidators:AbstractValidator<AddContactListCommand>
    {
        public AddContactListCommandValidators()
        {
            RuleFor(x => x.ListName)
           .NotEmpty().WithMessage("List Name is required.")
           .MaximumLength(100).WithMessage("List Name cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email Address.")
                .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");
        }
    }
}
