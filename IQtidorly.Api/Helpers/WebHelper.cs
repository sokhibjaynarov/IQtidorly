﻿using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace IQtidorly.Api.Helpers
{
    public static class WebHelper
    {
        public const string CLAIM_USER_ID = "UserId";

        public static Guid GetUserId(HttpContext httpContext)
        {
            var claims = httpContext.User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(l => l.Type == CLAIM_USER_ID);

            if (userIdClaim == null)
            {
                throw new Exception("claim UserId not found");
            }

            Guid userId = Guid.Parse(userIdClaim.Value);

            return userId;
        }
    }
}
