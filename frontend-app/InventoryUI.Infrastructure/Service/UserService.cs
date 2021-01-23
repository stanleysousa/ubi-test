using InventoryUI.Common.Model;
using InventoryUI.DataAccess.Repository.Interfaces;
using InventoryUI.Infrastructure.Entity;
using InventoryUI.Infrastructure.Service.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InventoryUI.Infrastructure.Service
{
    public class UserService : IUserService
    {
        IOptions<Config> _config;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IOptions<Config> config, IRoleRepository roleRepository, IUserRepository userRepository, IHttpClientFactory clientFactory)
        {
            _config = config;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _clientFactory = clientFactory;
        }

        public async Task<User> GetUserWithItems(int idUser)
        {
            try
            {
                var uri = _config.Value.UserServiceURL + idUser.ToString();
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var xml = new XmlSerializer(typeof(User)).Deserialize(responseStream);
                    var result = (User)xml;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Entity.UserInformation> GetUserByName(string usernaname)
        {
            try
            {
                //Get user info
                var userInfo = await _userRepository.GetUserByName(usernaname);
                if (userInfo is null)
                    return null;

                //Get user's role
                var roleInfo = await _roleRepository.GetRole(userInfo.IdRole);
                var role = new Role(roleInfo.IdRole, roleInfo.RoleDescription);

                var user = new Entity.UserInformation(role, userInfo.IdUser, userInfo.Username, userInfo.Password);

                return user;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Entity.UserInformation>> GetAllUsers()
        {
            var result = new List<Entity.UserInformation>();
            try
            {
                //Get user info
                var usersInfo = await _userRepository.GetAllUsers();

                foreach (var userInfo in usersInfo)
                {
                    var roleInfo = await _roleRepository.GetRole(userInfo.IdRole);
                    var role = new Role(roleInfo.IdRole, roleInfo.RoleDescription);
                    var user = new Entity.UserInformation(role, userInfo.IdUser, userInfo.Username, userInfo.Password);

                    result.Add(user);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }
    }
}
