using Inventory.DataAccess.DTO;
using Inventory.Common.Model;
using Inventory.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IOptions<Config> config, ILogger<UserRepository> logger)
        {
            _connectionString = config.Value.ConnectionString;
            _logger = logger;
        }
        public async Task<UserDTO> GetUser(int idUser)
        {
            UserDTO result = null;
            string query = @"SELECT * FROM user WHERE idUser = @idUser";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                cmd.Prepare();

                using var reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var userId = reader.GetInt32(0);
                    var username = reader.GetString(1);
                    result = new UserDTO(userId, username);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading User");
                throw;
            }

            return result;
        }
    }
}
