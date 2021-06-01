using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApi.Helpers;
using webApi.Models;

namespace webApi.MIddleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IO2_RestaurantsContext  dataContext)
        {
            var token = context.Request.Headers["api-key"].ToString();

            if (token != null && token != "")
                await attachAccountToContext(context, dataContext, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, IO2_RestaurantsContext dataContext, string token)
        {
            try
            {
               
                var info = token.Split(',');
                int accountId = int.Parse(info[0]);
                // attach account to context on successful jwt validation
                context.Items["Account"] = await dataContext.Users.FindAsync(accountId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}