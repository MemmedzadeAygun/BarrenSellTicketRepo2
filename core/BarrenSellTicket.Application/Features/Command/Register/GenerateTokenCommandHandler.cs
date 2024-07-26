using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, AuthenticatedUserDto>
    {
        private readonly IUnitOfWork _uow;

        public GenerateTokenCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AuthenticatedUserDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetUsers(request.Email, request.Password);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"));

            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Customer.FirstName),
                new Claim(ClaimTypes.Surname,user.Customer.LastName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials=credentials,
            };
        }
        
    }
}
