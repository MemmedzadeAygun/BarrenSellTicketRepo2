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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IEnumerable<RoleViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoleViewDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.RoleRepository.GetRoles();

            var roleViewDtos = _mapper.Map<IEnumerable<RoleViewDto>>(roles);

            return roleViewDtos;
        }
    }
}
