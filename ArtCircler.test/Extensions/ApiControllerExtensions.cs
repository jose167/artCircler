﻿using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace ArtCircler.test.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this ApiController controller, string userId)
        {
            var identity = new GenericIdentity(userId);
            identity.AddClaim(
                new Claim("http://squema.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }
    }
}
