using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OwnerPasswordCredentialsGrant
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<bool> ValidateUserNameAuthorizationPwdAsync(string userName, string pwd)
        {
            const string cmdText = @"SELECT COUNT(*) FROM [dbo].[users] WHERE username=@username AND pwd=@pwd";

            try
            {
                return await new SqlConnection(DbSetting.App).ExecuteScalarAsync<int>(cmdText, new { userName = userName, pwd = pwd }) != 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
