using AutoMapper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBankAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var existingBankAccount = await _unitOfWork.BankAccountRepository.GetByIdAsync(request.Id);
            if (existingBankAccount != null)
            {
                _mapper.Map(request, existingBankAccount);
                _unitOfWork.BankAccountRepository.Update(existingBankAccount);
            }
            else
            {
                var newBankAccount = _mapper.Map<BankAccount>(request);
                await _unitOfWork.BankAccountRepository.AddAsync(newBankAccount);
            }

            await _unitOfWork.Commit();
        }
    }
}
