﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.account
{
    public class ApplicationUserIdentity 
    {
        public int ApplicationUserId { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
    }
}
