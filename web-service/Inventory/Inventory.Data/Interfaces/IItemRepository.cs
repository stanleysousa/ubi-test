using Inventory.DataAccess.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Interfaces
{
    public interface IItemRepository
    {
        Task<List<ItemDTO>> GetUserItems(int idUser);
    }
}
