using Biblioteka_2.DemoData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka_2.Data
{
    class GetDeomoData
    {
        public enum Tables { Książki, Autorzy, Czytelnicy, KsiążkiAutorzy, NowiAutorzy, Wypożyczalnia }

        public async Task<List<DemoDelayed>> GetTable(string connectionString)
        {
            List<DemoDelayed> cos = new List<DemoDelayed>();
            string sql = "select * from [dbo].[delayedDonations]()";
            string sql2 = "select * from [dbo].[notDeliveredYet]()";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand commandDatabase = new SqlCommand(sql, cnn))
            {
                try
                {
                    SqlDataReader reader;
                    cnn.Open();
                    
                    reader = commandDatabase.ExecuteReader();
                    var g = reader.GetEnumerator();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            await Task.Run(() => cos.Add(new DemoDelayed { ISBN = reader.GetString(0), Tytuł = reader.GetString(1), Wydawca = reader.GetString(2), DataWypożyczenia = reader.GetDateTime(3), SpodziewanaDataZwrotu = reader.GetDateTime(4), Imię = reader.GetString(5), Nazwisko = reader.GetString(6) }));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return cos.OrderBy(x => x.DataWypożyczenia).ToList();
        }
        public string thisIsData(IProgress<int> progress)
        {
            string outout = null;
            string[] vs = "1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0".Split();
            for(int i = 0; i <= vs.Length - 1; i++)
            {
                outout += vs[i];
                System.Threading.Thread.Sleep(5);
                progress.Report((i * 100) / (vs.Length - 1));
            }
            return outout;
        }
    }
}
