using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebAPIs.Data
{
    public class Helper
    {
        private readonly ClaimsPrincipal principal;
        public Helper(IPrincipal _principal)
        {
            principal = _principal as ClaimsPrincipal;
        }
        public dynamic GetSpecificClaim(string type)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;
            var userName = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;
            if (type == "ID")
            {
                return Convert.ToInt32(userId);
            }
            else
            {
                return Convert.ToString(userName);
            }
        }
    }
}
