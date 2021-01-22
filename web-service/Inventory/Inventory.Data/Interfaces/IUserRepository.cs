using Inventory.DataAccess.DTO;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(int iduser);
    }
}
