using InventoryUI.DataAccess.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryUI.DataAccess.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserByName(string username);
        Task<List<UserDTO>> GetAllUsers();
    }
}
