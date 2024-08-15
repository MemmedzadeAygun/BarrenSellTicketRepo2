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
    public class AddEventCommandValidator:AbstractValidator<AddEventCommand>
    {
        public AddEventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Event name is required")
                .MaximumLength(100).WithMessage("Event name must be less than 100 characters.")
                .CheckNull();

            RuleFor(x => x.EventDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Event date must be today or in the future");

            RuleFor(x => x.BeginTime)
                .LessThan(x => x.EndTime).WithMessage("Begin time must be earlier than end time.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.BeginTime).WithMessage("End time must be later than begin time.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage("Country must be less than 100 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100).WithMessage("City must be less than 100 characters");

            RuleFor(x => x.Addres)
                .NotEmpty().WithMessage("Addres is required")
                .MaximumLength(100).WithMessage("Addres must be less than 100 characters");

            RuleFor(x => x.EventTypeId)
                .GreaterThan(0).WithMessage("Event type is required and must be greater than 0.");

            RuleFor(x => x.EventCategoryId)
                .GreaterThan(0).WithMessage("Category is required and must be greater than 0.");

            RuleFor(x => x.OrganizerDetailId)
                .GreaterThan(0).WithMessage("Organizer is required and must be greater than 0.");

            RuleFor(x => x.Description)
                 .NotEmpty().WithMessage("Description is required")
                 .MaximumLength(1000).WithMessage("Description must be less than 1000 characters.");
        }
    }
}
