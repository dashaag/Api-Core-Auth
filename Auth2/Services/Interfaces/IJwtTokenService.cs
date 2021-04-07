using Auth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
