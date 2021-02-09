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
        public enum Tytul
        {
            Pan,
            Pani
        };
        public enum Profil
        {
            Student,
            Dziecko,
            Dorosły
        }
        public async Task<List<Stats>> GetStats(string connectionString)
        {
            List<Stats> _output;
            string sql = @"select * from [dbo].[RentalsStats]()";
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                _output = cnn.QueryAsync<Stats>(sql).GetAwaiter().GetResult().ToList();
            }
            return _output;
        }
        public void NewRental(string connectionString,AvailableBooks book,ReaderModel reader,DateTime time)
        {
            var p = new
            {
                isbn = book.ISBN,
                Nr_Czytelnika = reader.Nr_czytelnika,
                spodziewanaDataZwrotu = time
            };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SpNewRental]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void NewAutor(string connectionString,string name,string lastname)
        {
            var p = new
            {
                imie = name,
                nazwisko = lastname
            };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SpAutor]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void NewBook(string connectionString,AvailableBooks book,SimlpeAthor athor)
        {
            var p = new
            {
                isbn = book.ISBN,
                tytul = book.Tytuł,
                rok_wydania = book.Rok_wydania,
                wydawca = book.Wydawca,
                kategoria =  book.Kategoria,
                nr_autora = athor.Nr_autora
            };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SpNewBook]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public List<SimlpeAthor> ListOfAuthors(string connectionString)
        {
            List<SimlpeAthor> output;
            string sql = "select * from [dbo].[ListOfAuthors]()";

            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                output = cnn.Query<SimlpeAthor>(sql).ToList();
            }
            return output;
        }

        public List<Categories> ListOfCategories(string connectionString)
        {
            List<Categories> output;
            string sql = "select * from GetCategories";

            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                output = cnn.Query<Categories>(sql).ToList();
            }
            return output;
        }
        public async void InsertHandingOverTheBook(string connectionString, DemoDelayedBase delayed)
        {
            var p = new
            {
                data = DateTime.Now,
                isbn = delayed.ISBN,
                data_Wypozyczenia = delayed.Data_Wypożyczenia,
                oplata = Convert.ToDecimal(delayed.oplata.Substring(0, (delayed.oplata.Length - 2)))
            };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SPhandingOverTheBook]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public async void delUser(string connectionString, ReaderModel reader)
        {
            var p = new
            {
                Nr_czytelnika = reader.Nr_czytelnika,
            };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SpDelUser]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public async void delBook(string connectionString, AvailableBooks book)
        {
                var p = new
                {
                    isbn = book.ISBN
                };
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("[dbo].[SpDelBook]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void AddReader(string connectionString,ReaderModel model)
        {
            string sql = "INSERT INTO dbo.Czytelnicy(Tytuł,Imię,Nazwisko,Adres,Miasto,Telefon,Email,Opłata,Wartość,Profil) " +
                "values(@Tytuł,@Imię,@Nazwisko,@Adres,@Miasto,@Telefon,@Email,@Opłata,@Wartość,@Profil)";

            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute(sql, model);
            }
        }
        public async Task<List<AvailableBooks>> GetAvailableBooks(string connectionString)
        {
            List<AvailableBooks> output;
            string sql = @"select * from [dbo].[availableBooks]()";
            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                output = cnn.QueryAsync<AvailableBooks>(sql).GetAwaiter().GetResult().ToList();
            }
            return output;
        }

        public async Task<List<ReaderModel>> GetReaders(string connectionString)
        {
            List<ReaderModel> output;
            string sql = "select * from [dbo].[GetCzytelnicy]";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                output = connection.QueryAsync<ReaderModel>(sql).GetAwaiter().GetResult().ToList();
            }
            return output;
        }
        public async Task<List<DemoDelayedBase>> GetNotDelivered(string connectionString,bool only)
        {
            List<DemoDelayed> temp;
            List<DemoDelayedBase> output = new List<DemoDelayedBase>();
            string sql = "select * from [dbo].[notDeliveredYet]()";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                temp = connection.QueryAsync<DemoDelayed>(sql).GetAwaiter().GetResult().ToList();
                Parallel.ForEach(temp, (item) =>
                {
                    item.Autor = $"{item.Imie_autora} {item.Nazwisko_autora}";
                    if (DateTime.Now > item.Spodziewana_Data_Zwrotu)
                    {
                        float oplata = ((DateTime.Now - item.Spodziewana_Data_Zwrotu).Days) * 0.3f;
                        item.oplata = ((float)Math.Round(oplata * 100f) / 100f).ToString("C2");
                        item.dni = (DateTime.Now - item.Spodziewana_Data_Zwrotu).Days;
                        output.Add(item);
                    }
                    else if(!only)
                        output.Add(item);
                });
            }
            return output;
        }
        public string thisIsData(IProgress<int> progress)
        {
            string outout = null;
            for(int i = 0; i <= 500; i++)
            {
                Thread.Sleep(1);
                int temp = (i * 100) / 500;
                progress.Report(temp);
            }
            return outout;
        }
    }
}
