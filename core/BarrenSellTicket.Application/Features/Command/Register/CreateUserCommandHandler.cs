using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Helper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthenticatedUserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Users>(request);
            user.Customer = _mapper.Map<Customer>(request);

            byte[] passwordHash, passwordSalt;

            HasHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            await _unitOfWork.UserRepository.AddUser(user);
            await _unitOfWork.Commit();
            return new AuthenticatedUserDto();
        }
    }
}
