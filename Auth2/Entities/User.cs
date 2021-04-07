﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2.Entities
{
    public class User: IdentityUser
    {
        public virtual UserAdditionalInfo UserAdditionalInfo { get; set; }

    }
}
