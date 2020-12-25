using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Biblioteka_2.Data
{
    class GetData
    {
        public enum Tables { Książki, Autorzy, Czytelnicy, KsiążkiAutorzy, NowiAutorzy, Wypożyczalnia }

        //public IList<cos> GetTable(string connectionString, object tabeleStruct, Tables table)
        //{
        //    List<cos> cos = new List<cos>();
        //    string sql = $"select * from {table}";

        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    using (SqlCommand commandDatabase = new SqlCommand(sql, cnn))
        //    {
        //        try
        //        {
        //            SqlDataReader reader;
        //            cnn.Open();
        //            reader = commandDatabase.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    cos.Add(new cos { tytul = reader.GetString(1), rok_wydania = reader.GetInt32(2), wydawca = reader.GetString(3), kategoria = reader.GetString(4) });
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //    }
        //    return cos;
        //}
        public string thisIsData(Action<int> action)
        {
            string outout = null;
            string[] vs = "1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0".Split();
            for(int i = 0; i <= vs.Length - 1; i++)
            {
                outout += vs[i];
                System.Threading.Thread.Sleep(5);
                action.Invoke(vs.Length);
            }
            return outout;
        }
    }
}
