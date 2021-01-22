using Inventory.Domain.Entity;
using System.Threading.Tasks;

namespace Inventory.Domain.Service.Interface
{
    public interface IUserService
    {
        Task<User> GetUserItemsAsync(int idUser);
    }
}
