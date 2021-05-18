using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using webApi.DataTransferObjects.AuthenticateDTO;
using webApi.Models;
using webApi.Helpers;
using webApi.Exceptions;

namespace webApi.Services
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);

    }

    public class AccountService : IAccountService
    {
        private readonly IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        //private readonly IEmailService _emailService;

        public AccountService(
            IO2_RestaurantsContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)

        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;

        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            if (account == null || !BC.Verify(model.Password, account.PasswordHash))  
                throw new UnathorisedException("Email or password is incorrect");
            var jwtToken = generateJwtToken(account);
            var response = _mapper.Map<AuthenticateResponse>(account);
            response.Token = jwtToken;
            return response;
        }
        private string generateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

     
       
      
    }
}