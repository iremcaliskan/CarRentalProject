using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    { // Access Token for user
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
// Future Ability Refresh Token, avoiding the repated login