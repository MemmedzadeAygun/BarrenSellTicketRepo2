using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Validators.FluentValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application
{
    public static class ServiceRegistrations
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(x=>x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddScoped(typeof(AbstractValidator<AddBankAccountCommand>),typeof(AddBankAccountCommandValidator));
            services.AddScoped(typeof(AbstractValidator<AddContactListCommand>), typeof(AddContactListCommandValidators));
            services.AddScoped(typeof(AbstractValidator<AddCouponCommand>),typeof(AddCouponCommandValidator));
            services.AddScoped(typeof(AbstractValidator<AddEventCommand>), typeof(AddEventCommandValidator));
            services.AddScoped(typeof(AbstractValidator<AddOrganizerDetailCommand>), typeof(AddOrganizerDetailCommandValidator));
        }
    }
}
