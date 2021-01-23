using InventoryUI.Common.Model;
using InventoryUI.DataAccess.DTO;
using InventoryUI.DataAccess.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace InventoryUI.DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(IOptions<Config> config, ILogger<RoleRepository> logger)
        {
            _connectionString = config.Value.ConnectionString;
            _logger = logger;
        }

        public async Task<RoleDTO> GetRole(int idRole)
        {
            RoleDTO result = null;
            string query = @"SELECT * FROM role WHERE idRole = @idRole";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idRole", idRole);
                cmd.Prepare();

                using var reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var roleId = reader.GetInt32(0);
                    var description = reader.GetString(1);
                    result = new RoleDTO(roleId, description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Role");
                throw;
            }

            return result;
        }
    }
}
