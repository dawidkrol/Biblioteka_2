using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteka_2
{
    class LoginTest : ILogin
    {
        public List<T> getUsers<T>(IModule _module) where T : IUserProfile, new()
        {
            List<T> output = new List<T>();
            output.Add(new T { _login = "test", _password = "test" });
            return output;
        }
    }
}
