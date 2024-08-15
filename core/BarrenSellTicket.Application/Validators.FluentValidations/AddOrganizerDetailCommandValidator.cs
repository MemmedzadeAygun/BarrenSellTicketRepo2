using BarrenSellTicket.Application.Features.Command.Others;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Validators.FluentValidations
{
    public class AddOrganizerDetailCommandValidator:AbstractValidator<AddOrganizerDetailCommand>
    {
        public AddOrganizerDetailCommandValidator()
        {
            RuleFor(organizer => organizer.Name)
                .NotEmpty().WithMessage("Organizer Name is required")
                .MaximumLength(50).WithMessage("Organizer name must be less than 100 characters.");

            RuleFor(organizer => organizer.About)
                .NotEmpty().WithMessage("About is required")
                .MaximumLength(1000).WithMessage("About must be less than 1000 characters.");

            RuleFor(organizer => organizer.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^(\+994|0)(50|51|55|70|77)\d{7}$")
                .WithMessage("Phone number must be a valid Azerbaijani number. Example: +994501234567 or 0501234567");

            RuleFor(organizer=>organizer.Country)
                 .NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage("Country must be less than 100 characters");

            RuleFor(organizer=>organizer.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100).WithMessage("City must be less than 100 characters");

            RuleFor(organizer=>organizer.Addres)
                .NotEmpty().WithMessage("Addres is required")
                .MaximumLength(100).WithMessage("Addres must be less than 100 characters");

            RuleFor(organizer=>organizer.Description)
                .NotEmpty().WithMessage("Description is required")
                 .MaximumLength(1000).WithMessage("Description must be less than 1000 characters.");
        }
    }
}
