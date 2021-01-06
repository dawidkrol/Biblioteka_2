using System.Data.SqlClient;

namespace Biblioteka_2
{
    public interface ISqlProfile
    {
        SqlConnectionStringBuilder connectionString { get; }
        string databaseName { get;  }
        string ip { get;  }
        string name { get;  }
        string password { get; }
    }
}