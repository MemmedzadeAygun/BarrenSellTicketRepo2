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
    public class GetBankAccountQueryHandler : IRequestHandler<GetBankAccountQuery, IEnumerable<BankAccountViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBankAccountQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BankAccountViewDto>> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
        {
            var bankAccounts = await _unitOfWork.BankAccountRepository.GetAllAsync();

            var bankAccountViewDtos = new List<BankAccountViewDto>();

            foreach (var bankAccount in bankAccounts)
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(bankAccount.CustomerId.Value);

                bankAccount.Customer = customer;

                var dto = new BankAccountViewDto
                {
                    BankName = bankAccount.BankName,
                    AccountNumber = bankAccount.AccountNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };

                bankAccountViewDtos.Add(dto);
            }

            return bankAccountViewDtos;
        }
    }
}
