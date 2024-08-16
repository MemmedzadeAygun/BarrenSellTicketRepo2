using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, BankAccountDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBankAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BankAccountDto> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = await _unitOfWork.BankAccountRepository.GetById(request.bankAccountId);
            if (bankAccount == null)
            {
                return null;
            }

            var bankAccountDto = _mapper.Map<BankAccountDto>(bankAccount);
            bankAccountDto.Customer = _mapper.Map<CustomerDto>(bankAccount.Customer);
            return bankAccountDto;
        }
    }
}
