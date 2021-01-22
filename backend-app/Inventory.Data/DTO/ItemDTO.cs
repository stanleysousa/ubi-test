using System;

namespace Inventory.DataAccess.DTO
{
    public class ItemDTO
    {
        public int IdItem { get; private set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }

        public ItemDTO(int idItem, int idUser, string name, string game, DateTime expirationDate, int quantity)
        {
            IdItem = idItem;
            IdUser = idUser;
            Name = name;
            Game = game;
            ExpirationDate = expirationDate;
            Quantity = quantity;
        }
    }
}
