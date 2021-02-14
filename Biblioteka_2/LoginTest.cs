using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteka_2
{
    class LoginTest : ILogin
    {
        public async Task<List<T>> getUsers<T>(IModule _module) where T : IUserProfile, new()
        {
            List<T> output = new List<T>();
            output.Add(new T { login = "test", password = "test" });
            return output;
        }

        public List<UserProfile> getUsers(IModule _module)
        {
            List<UserProfile> output = new List<UserProfile>();
            output.Add(new UserProfile { login = "test", password = "test" });
            return output;
        }
    }
}
