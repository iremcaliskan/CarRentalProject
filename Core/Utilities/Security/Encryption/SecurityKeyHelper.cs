using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) 
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); // Create a symmetric key for TokenOptions' SecurityKey in appsettings.json
        }
    }
}
