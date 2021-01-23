using InventoryUI.Common.Model;
using InventoryUI.DataAccess.DTO;
using InventoryUI.DataAccess.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryUI.DataAccess.Repository
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

        public async Task<UserDTO> GetUserByName(string username)
        {
            UserDTO result = null;
            string query = @"SELECT * FROM user WHERE username = @username";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Prepare();

                using var reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var userId = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var password = reader.GetString(2);
                    var roleId = reader.GetInt32(3);
                    result = new UserDTO(roleId, userId, name, password);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading User");
                throw;
            }

            return result;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var result = new List<UserDTO>();
            string query = @"SELECT * FROM user";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);

                using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var userId = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var password = reader.GetString(2);
                    var roleId = reader.GetInt32(3);
                    var user = new UserDTO(roleId, userId, name, password);
                    result.Add(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Users");
                throw;
            }

            return result;
        }
    }
}
