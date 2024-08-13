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
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, List<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CustomerDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var userDetail = await _unitOfWork.CustomerRepository.GetAllAsync();
            var userDetailDto=_mapper.Map<List<CustomerDto>>(userDetail);

            return userDetailDto;
        }
    }
}
