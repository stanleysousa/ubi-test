using Inventory.DataAccess.Interfaces;
using Inventory.Domain.Entity;
using Inventory.Domain.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IItemRepository itemRepository, IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
        }

        public async Task<User> GetUserItemsAsync(int idUser)
        {
            try
            {
                //Get user info
                var userInfo = await _userRepository.GetUser(idUser);
                if (userInfo is null)
                    return null;

                var user = new User
                {
                    username = userInfo.Username
                };

                //Get user's items info
                var itemsInfo = await _itemRepository.GetUserItems(idUser);
                if (itemsInfo.Count == 0)
                    return user;

                var items = new Dictionary<int, Item>();
                foreach (var itemInfo in itemsInfo)
                {
                    //Add the game to the item's game array
                    if (items.TryGetValue(itemInfo.IdItem, out var item))
                    {
                        for (int i = 0; i < item.game.Length; i++)
                            if (item.game[i] == null)
                            {
                                item.game[i] = itemInfo.Game;
                                break;
                            }
                    }
                    else
                    {
                        var itemCount = itemsInfo.FindAll(i => itemInfo.IdItem == i.IdItem).Count();

                        var newItem = new Item();
                        newItem.name = itemInfo.Name;
                        newItem.game = new string[itemCount];
                        newItem.game[0] = itemInfo.Game;
                        newItem.expirationDate = itemInfo.ExpirationDate;
                        newItem.quantity = itemInfo.Quantity.ToString();

                        items.Add(itemInfo.IdItem, newItem);
                    }
                }

                //Get items' property info
                for (int i = 0; i < items.Keys.Count(); i++)
                {
                    var selectedItem = items.ElementAt(i);
                    var props = await _propertyRepository.GetProperties(selectedItem.Key);

                    if (props.Count() > 0)
                        selectedItem.Value.property = new Property[props.Count];

                    for (int j = 0; j < props.Count(); j++)
                        if (selectedItem.Value.property[j] == null)
                        {
                            var prop = new Property();
                            prop.name = props[j].Name;
                            prop.value = props[j].Value;
                            selectedItem.Value.property[j] = prop;
                            break;
                        }
                }

                user.item = items.Values.ToArray();
                return user;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
