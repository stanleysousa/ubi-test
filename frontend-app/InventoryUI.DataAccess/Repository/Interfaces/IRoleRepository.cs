using InventoryUI.DataAccess.DTO;
using System.Threading.Tasks;

namespace InventoryUI.DataAccess.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleDTO> GetRole(int idRole);
    }
}
