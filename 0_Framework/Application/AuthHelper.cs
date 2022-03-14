using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthViewModel CurrentUserInfo()
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();

            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "UserId").Value);
            result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.UserName = claims.FirstOrDefault(x => x.Type == "UserName").Value;
            result.Role = claims.FirstOrDefault(x => x.Type == "RoleTitle").Value;

            var permissions = claims.FirstOrDefault(x => x.Type == "Permissions")?.Value;

            result.Permissions = JsonConvert.DeserializeObject<List<int>>(permissions);
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            return result;
        }

        public string CurrentUserRole()
        {
            if (IsAuthenticated())
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            return null;
        }

        public List<int> GetCurrentUserPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")?.Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public void SignIn(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("UserId",account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim("UserName",account.UserName), // or: ClaimTypes.NameIdentifier
                new Claim("Permissions", permissions),
                new Claim(ClaimTypes.Role,account.RoleId.ToString()),
                new Claim("RoleTitle", account.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = account.RememberMe
            };

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, authProperties);
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
