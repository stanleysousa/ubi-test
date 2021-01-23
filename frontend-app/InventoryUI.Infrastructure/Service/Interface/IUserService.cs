using InventoryUI.Infrastructure.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryUI.Infrastructure.Service.Interface
{
    public interface IUserService
    {
        Task<User> GetUserWithItems(int idUser);
        Task<Entity.UserInformation> GetUserByName(string usernaname);
        Task<List<Entity.UserInformation>> GetAllUsers();
    }
}
