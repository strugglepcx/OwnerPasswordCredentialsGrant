using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace OwnerPasswordCredentialsGrant
{
    public class ClientAuthorizationRepository : IClientAuthorizationRepository
    {
        public async Task<string> GenerateOAuthClientSecretAsync(string client_id = "")
        {
            return await Task.Run(() =>
            {
                var salt = new byte[32];
                if (client_id.IsNotNullOrEmpty())
                {
                    salt = client_id.ToByteArray();
                }

                using (var provider = new RNGCryptoServiceProvider())
                {
                    provider.GetBytes(salt);
                }

                return Convert.ToBase64String(salt).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            });
        }

        public async Task<bool> SaveTokenAsync(Token token)
        {
            const string cmdText = @"INSERT INTO Tokens(clientId,userName,accessToken ,refreshToken,issuedUtc ,expiresUtc,IpAddress) VALUES(@clientId,@userName,@accessToken ,@refreshToken,@issuedUtc ,@expiresUtc,@IpAddress)";
            try
            {
                // return await new SqlConnection(DbSetting.App).InsertAsync(token) != 0;
                return await new SqlConnection(DbSetting.App).ExecuteAsync(cmdText, new
                {
                    clientId = token.ClientId,
                    userName = token.UserName,
                    accessToken = token.AccessToken,
                    refreshToken = token.RefreshToken,
                    issuedUtc = token.IssuedUtc,
                    expiresUtc = token.ExpiresUtc,
                    IpAddress = token.IpAddress

                }) != 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ValidateClientAuthorizationSecretAsync(string client_id, string client_secret)
        {
            const string cmdText = @"SELECT COUNT(*) FROM [dbo].[clients] WHERE client_id=@clientId AND client_secret=@clientSecret";
            try
            {
                return await new SqlConnection(DbSetting.App).ExecuteScalarAsync<int>(cmdText, new { clientId = client_id, clientSecret = client_secret }) != 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
