using BarrenSellTicket.Application.Exception;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Extensions
{
    public static class ValidationExtension
    {
        public static async Task ThrowIfValidationFailAsync<T>(this IValidator<T> validator, T instance)
        {
            var validationResult=await validator.ValidateAsync(instance);
            if (!validationResult.IsValid)
            {
                throw new BarrenSellTicketValidationException(validationResult.Errors);
            }
        }

        public static IRuleBuilderOptions<T,TProperty> CheckNull<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.WithMessage($" Data  is required");
        }
    }
}
