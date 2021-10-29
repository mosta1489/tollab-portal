using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Tolab.Common
{
    public interface ISessionManager
    {
        public string AccessToken { get; }
        public string UserId { get; }
        public int CountryId { get; }
        public string CountryCode { get; }
        public bool? HasInterests { get; }
        public string UserPhoto { get; }
    }

    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string AccessToken
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
                }

                return null;
            }
        }

        public string UserId
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                }

                return null;
            }
        }

        public string UserName
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
                }

                return null;
            }
        }

        public string UserPhoto
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserPhoto")?.Value;
                }

                return null;
            }
        }

        public int CountryId
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CountryId")?.Value);
                }

                return 0;
            }
        }

        public string CountryCode
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CountryCode")?.Value;
                }

                return null;
            }
        }

        public bool? HasInterests
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null
                    && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var hasInterests = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "HasInterests")?.Value;

                    if (hasInterests == null)
                        return false;

                    return bool.Parse(hasInterests);
                }

                return null;
            }
        }
    }
}