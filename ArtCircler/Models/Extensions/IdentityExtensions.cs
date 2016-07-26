using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ArtCircler.Models.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity) identity).FindFirst("Name");
                return (claim != null) ? claim.Value : string.Empty;
            }


        public static string GetUserBio(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst("Bio");
            return (claim != null) ? claim.Value : string.Empty;

        }
    }
    
}