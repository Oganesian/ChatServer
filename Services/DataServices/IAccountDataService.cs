using AccountAndConnection;
using System.Threading.Tasks;

namespace Services.DataServices
{
    public interface IAccountDataService : IDataService<BaseAccount>
    {
        Task<BaseAccount> GetByUsername(string username);
        Task<BaseAccount> GetByEmail(string email);
    }
}
