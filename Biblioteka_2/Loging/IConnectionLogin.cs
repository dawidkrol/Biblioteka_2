namespace Biblioteka_2
{
    public interface IConnectionLogin
    {
        ISqlProfile Login(string ip, string databaseName, string userName, string password);
    }
}