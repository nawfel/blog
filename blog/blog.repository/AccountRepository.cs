using blog.models.account;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blog.repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;
        public AccountRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var dataTable =new System.Data.DataTable();
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("NormalizedUsername", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(user.UserName, user.NormalizedUserName, user.Email, user.NormalizedEmail, user.FullName, user.PasswordHash);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync("Account_Insert",
                    new {Account =dataTable.AsTableValuedParameter("dbo.AccountType")},commandType:System.Data.CommandType.StoredProcedure );
            }
            return IdentityResult.Success;
        }

        public async Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedname, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUser;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))

            {
                await connection.OpenAsync(cancellationToken);
                applicationUser = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>(
                       "Account_GetByUsername", new { NormalizedUsername = normalizedname },
                       commandType: CommandType.StoredProcedure
                       );
            }

            return applicationUser;
        }

        public async Task<ApplicationUserIdentity> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUser;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))

            {
                await connection.OpenAsync(cancellationToken);
                applicationUser = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>(
                       "Account_GetByEmail", new { Email = email },
                       commandType: CommandType.StoredProcedure
                       );
            }

            return applicationUser;
        }

       
    }
}
