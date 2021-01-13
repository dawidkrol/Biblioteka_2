using Biblioteka_2.DemoData;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteka_2.Data
{
    public class GetDeomoData
    {
        public enum Tables { Książki, Autorzy, Czytelnicy, KsiążkiAutorzy, NowiAutorzy, Wypożyczalnia }

        public async Task<List<DemoDelayed>> GetTable(string connectionString)
        {
            string sql = "select * from [dbo].[delayedDonations]()";

            using(IDbConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.QueryAsync<DemoDelayed>(sql).GetAwaiter().GetResult().ToList();
                return output;
            }
        }
        public string thisIsData(IProgress<int> progress)
        {
            string outout = null;
            for(int i = 0; i <= 100 - 1; i++)
            {
                Thread.Sleep(5);
                progress.Report((i * 100) / (100));
            }
            return outout;
        }
    }
}
