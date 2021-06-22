using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Biblioteka_2
{
    public interface IModule
    {
        bool _login { get; set; }
        bool IsLogged { get; set; }
        ISqlProfile SqlProfile { get;}
        public IUserProfile user { get; }

        event PropertyChangedEventHandler PropertyChanged;

        bool UpdateSqlProfile(string ip, string databaseName, string userName, string password);
        void logout();
        public Task<bool> login<T>(string name, string password, object sender) where T : IUserProfile, new();
    }
}