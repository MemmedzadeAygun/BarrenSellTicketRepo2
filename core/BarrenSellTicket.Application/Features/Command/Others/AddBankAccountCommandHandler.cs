using AutoMapper;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<AddBankAccountCommand> validationRules;

        public AddBankAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, AbstractValidator<AddBankAccountCommand> validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.validationRules = validationRules;
        }

        public async Task Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            await validationRules.ThrowIfValidationFailAsync(request);
            var bankAccount=_mapper.Map<BankAccount>(request);
            await _unitOfWork.BankAccountRepository.AddAsync(bankAccount);
            await _unitOfWork.Commit();
        }
    }
}
