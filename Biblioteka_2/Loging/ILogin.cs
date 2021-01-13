using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteka_2
{
    public interface ILogin
    {
        Task<List<T>> getUsers<T>(IModule _module) where T : IUserProfile, new();
    }
}