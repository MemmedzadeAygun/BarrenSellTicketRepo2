using BarrenSellTicket.Application.Features.Command.Others;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Validators.FluentValidations
{
    public class AddCouponCommandValidator:AbstractValidator<AddCouponCommand>
    {
        public AddCouponCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code cannot be empty")
                .Length(1, 50).WithMessage("Code must be between 1 and 50 characters");
        }
    }
}
