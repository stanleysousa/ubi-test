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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<PropertyRepository> _logger;

        public PropertyRepository(IOptions<Config> config, ILogger<PropertyRepository> logger)
        {
            _connectionString = config.Value.ConnectionString;
            _logger = logger;
        }

        public async Task<List<PropertyDTO>> GetProperties(int idItem)
        {
            var result = new List<PropertyDTO>();
            string query = @"SELECT * FROM property WHERE idItem = @idItem";

            try
            {
                using var con = new MySqlConnection(_connectionString);
                con.Open();

                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idItem", idItem);
                cmd.Prepare();

                using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var propId = reader.GetInt32(0);
                    var itemId = reader.GetInt32(1);
                    var name = reader.GetString(2);
                    var value = reader.GetString(3);
                    result.Add(new PropertyDTO(propId, itemId, name, value));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Properties");
                throw;
            }

            return result;
        }
    }
}
