using System;
using System.IO;

namespace Biblioteka_2
{
    class FileConfig : IFileConfig
    {
        public bool fileEx(string path)
        {
            bool output = false;
            FileInfo info = new FileInfo(path);
            output = info.Exists;
            return output;
        }

        public void CreateConfigFile(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Create().Close();
        }

        public void ConfigWrite(ISqlProfile sqlProfile, string path)
        {
            CreateConfigFile(path);
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.Write($"{sqlProfile.name},{sqlProfile.password},{sqlProfile.ip},{sqlProfile.databaseName}");
            streamWriter.Close();
        }

        public ISqlProfile GetInfoSQL(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                string info = reader.ReadLine();
                reader.Close();
                string[] vs = info.Split(',');
                ISqlProfile output = new SqlProfile(vs[0], vs[1], vs[3], vs[2]);
                return output;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
