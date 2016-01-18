using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerPasswordCredentialsGrant
{
    public interface IClientAuthorizationRepository
    {
        Task<string> GenerateOAuthClientSecretAsync(string client_id = "");
        Task<bool> ValidateClientAuthorizationSecretAsync(string client_id, string client_secret);
        Task<bool> SaveTokenAsync(Token token);
    }
}
