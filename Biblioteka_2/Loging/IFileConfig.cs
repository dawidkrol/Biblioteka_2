namespace Biblioteka_2
{
    public interface IFileConfig
    {
        void ConfigWrite(ISqlProfile sqlProfile, string path);
        void CreateConfigFile(string path);
        ISqlProfile GetInfoSQL(string path);
        bool fileEx(string path);
    }
}