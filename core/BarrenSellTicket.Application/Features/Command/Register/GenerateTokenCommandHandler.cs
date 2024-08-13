using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Helper;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

            var user = await _uow.UserRepository.GetUsers(request.Email);

            if (user is null)
            {
                throw new SellTicketException("Email not found");
            }
          
            if (!HasHelper.VerifyPasswordHash(request.Password,
                Convert.FromBase64String(user.PasswordHash),
                Convert.FromBase64String(user.PasswordSalt)))
            {
                throw new SellTicketException("password is incorrect");  //FluentValidation'la tamamla
            }

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

            var tokenHandler = new JwtSecurityTokenHandler();

            var token= tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticatedUserDto
            {
                Token = tokenHandler.WriteToken(token)
            };     
        } 
    }
}
