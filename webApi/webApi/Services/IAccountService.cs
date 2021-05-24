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
using webApi.Enums;

namespace webApi.Services
{
    public interface IAccountService
    {
        string Authenticate(AuthenticateRequest model);

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
        public string Authenticate(AuthenticateRequest model)
        {
            var account = _context.Users.SingleOrDefault(x => x.Email == model.Email);
            string apiToken = account.Id.ToString() + ", " + Enum.GetName(typeof(Role),account.Role);
          
            return apiToken;
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