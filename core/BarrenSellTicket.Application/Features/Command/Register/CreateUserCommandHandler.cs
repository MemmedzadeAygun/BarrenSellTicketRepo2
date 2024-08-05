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
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<AuthenticatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            //Create Address
            var address = _mapper.Map<Address>(request);
            await _unitOfWork.AddressRepository.AddAsync(address);
            await _unitOfWork.Commit();

            //Create Customer
            var customer = _mapper.Map<Customer>(request);
            customer.AddressId = address.Id;

            //Create Users
            var user = _mapper.Map<Users>(request);
            user.Customer = customer;

            byte[] passwordHash, passwordSalt;

            HasHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            //user.UserRoles = new List<UserRole>()
            //{
            //    new UserRole{RoleId  = 3}
            //};

            await _unitOfWork.UserRepository.AddUser(user);
            await _unitOfWork.Commit();

            return await _mediator.Send(new GenerateTokenCommand
            {
                Email = request.Email,
                Password = request.Password
            });

        }
    }
}
