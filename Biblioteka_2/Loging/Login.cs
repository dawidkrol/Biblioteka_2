using System.Collections.Generic;
using System.Data.SqlClient;

namespace Biblioteka_2
{
    class Login : ILogin
    {
        public List<T> getUsers<T>(IModule _module) where T : IUserProfile, new()
        {
            List<T> output = null;
            string sql = "select * from Users";
            try
            {
                output = new List<T>();
                using (SqlConnection cnn = new SqlConnection(_module.SqlProfile.connectionString.ToString()))
                using (SqlCommand cmm = new SqlCommand(sql, cnn))
                {
                    SqlDataReader reader = null;
                    cnn.Open();
                    reader = cmm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string login = reader.GetString(1);
                            string pass = reader.GetString(2);
                            string fn = reader.GetString(3);
                            string ln = reader.GetString(4);
                            output.Add(new T() { _FirstName = fn, _LastName = ln, _login = login, _password = pass });
                        }
                    }
                }
                return output;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
