using AutoMapper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _unitOfWork.RoleRepository.GetRoleById(request.Id);

            if (existingRole != null)
            {
                _mapper.Map(request, existingRole);
                _unitOfWork.RoleRepository.UpdateRole(existingRole);
            }
            else
            {
                var newRole = _mapper.Map<Role>(request);
                await _unitOfWork.RoleRepository.AddRole(newRole);
            }

            await _unitOfWork.Commit();

        }
    }
}
