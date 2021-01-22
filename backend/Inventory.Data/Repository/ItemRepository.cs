using Inventory.Common.Model;
using Inventory.DataAccess.DTO;
using Inventory.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(IOptions<Config> config, ILogger<ItemRepository> logger)
        {
            _connectionString = config.Value.ConnectionString;
            _logger = logger;
        }

        public async Task<List<ItemDTO>> GetUserItems(int idUser)
        {
            var result = new List<ItemDTO>();
            string query = @"SELECT * FROM item WHERE idUser = @idUser";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                cmd.Prepare();

                using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var itemId = reader.GetInt32(0);
                    var userId = reader.GetInt32(1);
                    var name = reader.GetString(2);
                    var game = reader.GetString(3);
                    var expiration = reader.GetDateTime(4);
                    var quantity = reader.GetInt32(5);
                    result.Add(new ItemDTO(itemId, userId, name, game, expiration, quantity));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Items");
                throw;
            }

            return result;
        }
    }
}
