using blog.models.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.services
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUserIdentity applicationUserIdentity);
    }
}
