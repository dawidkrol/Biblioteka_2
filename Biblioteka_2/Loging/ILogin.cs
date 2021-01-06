using System.Collections.Generic;

namespace Biblioteka_2
{
    public interface ILogin
    {
        List<T> getUsers<T>(IModule _module) where T : IUserProfile, new();
    }
}