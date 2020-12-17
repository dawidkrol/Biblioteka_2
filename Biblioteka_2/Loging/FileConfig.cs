using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Biblioteka_2
{
    class FileConfig
    {

        public SqlProfile getSqlProfile()
        {
            SqlProfile output = null;
            GetInfo();
            return output;
        }

        public static bool fileEx()
        {
            bool output = false;
            FileInfo info = new FileInfo("_config.txt");
            output = info.Exists;
            return output;
        }

        public void CreateConfigFile()
        {
            FileInfo file = new FileInfo("_config.txt");
            file.Create().Close();
        }

        public void ConfigWrite(SqlProfile sqlProfile)
        {
            StreamWriter streamWriter = new StreamWriter("_config.txt");
            streamWriter.Write($"{sqlProfile.name},{sqlProfile.password},{sqlProfile.ip},{sqlProfile.databaseName}");
            streamWriter.Close();
        }

        public SqlProfile GetInfo()
        {
            try
            {
                StreamReader reader = new StreamReader("_config.txt");
                string info = reader.ReadLine();
                reader.Close();
                string[] vs = info.Split(',');
                SqlProfile output = new SqlProfile(vs[0], vs[1], vs[3], vs[2]);
                return output;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
