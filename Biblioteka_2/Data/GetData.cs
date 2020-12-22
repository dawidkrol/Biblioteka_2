using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
    }
}
