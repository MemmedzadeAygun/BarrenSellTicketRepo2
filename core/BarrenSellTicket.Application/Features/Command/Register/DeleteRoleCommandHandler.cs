using BarrenSellTicket.Application.Exception;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role =await _unitOfWork.RoleRepository.GetRoleById(request.Id);
            if (role is null)
            {
                throw new SellTicketException("Id not found");
            }

             _unitOfWork.RoleRepository.Remove(role);

            await _unitOfWork.Commit();

        }
    }
}
