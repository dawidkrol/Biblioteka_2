using System.Collections.Generic;
using System.Data.SqlClient;

namespace Biblioteka_2
{
    class Login
    {
        public static List<UserProfile> getUsers(Module _module)
        {
            List<UserProfile> output = null;
            string sql = "select * from Users";
            try
            {
                output = new List<UserProfile>();
                using (SqlConnection cnn = new SqlConnection(_module.connectionstring))
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
                            output.Add(new UserProfile(fn, ln, pass, login));
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
