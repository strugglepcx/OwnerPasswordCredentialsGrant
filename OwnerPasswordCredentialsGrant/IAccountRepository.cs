using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerPasswordCredentialsGrant
{
    public interface IAccountRepository
    {
        Task<bool> ValidateUserNameAuthorizationPwdAsync(string userName, string pwd);
    }
}
