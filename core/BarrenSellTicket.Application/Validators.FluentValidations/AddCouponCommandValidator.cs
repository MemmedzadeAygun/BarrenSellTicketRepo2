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

            RuleFor(x => x.Discount)
                .NotEmpty().WithMessage("Discount cannot be empty")
                .GreaterThan(0).WithMessage("Discount must be greater than 0.");

            RuleFor(x => x.DiscountType)
                .IsInEnum().WithMessage("DiscountType must be either 1 (Percent) or 2 (FixedPrice).");

            RuleFor(x => x.DiscountEnd)
                .NotEmpty().WithMessage("DiscountEnd cannot be empty");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Time cannot be empty");

            RuleFor(coupon => (int)coupon.DiscountType)
                .InclusiveBetween(1, 2).WithMessage("DiscountType must be either 1 (Percent) or 2 (FixedPrice).");
        }
    }
}
