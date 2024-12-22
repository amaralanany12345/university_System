using System;
using SchoolSystem.Models;
using System.Security.Claims;

namespace SchoolSystem.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int getCurrentUserId()
        {
            if (_httpContextAccessor?.HttpContext?.User == null)
            {
                throw new ArgumentException("invalid http Context");
            }
            var stringUserId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            if (stringUserId == null)
            {
                throw new ArgumentException("user id is not found");
            }
            var userId = int.Parse(stringUserId);
            return userId;
        }

    }
}
