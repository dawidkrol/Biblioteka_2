using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Biblioteka_2
{
    class Login : ILogin
    {
        public async Task<List<T>> getUsers<T>(IModule _module) where T : IUserProfile, new()
        {
            List<T> output = null;
            string sql = "select * from Users";
            try
            {
                using (IDbConnection connection = new SqlConnection(_module.SqlProfile.connectionString.ToString()))
                {
                    var input = await connection.QueryAsync<T>(sql);
                    output = input.AsList();
                    return output;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
