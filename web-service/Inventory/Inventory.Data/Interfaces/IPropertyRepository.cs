using Inventory.DataAccess.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<PropertyDTO>> GetProperties(int idItem);
    }
}
