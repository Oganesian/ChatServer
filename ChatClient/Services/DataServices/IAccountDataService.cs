using AccountAndConnection;
using System.Threading.Tasks;

namespace ChatClient.Services.DataServices
{
    public interface IAccountDataService : IDataService<BaseAccount>
    {
        Task<BaseAccount> GetByUsername(string username);
        Task<BaseAccount> GetByEmail(string email);
    }
}
