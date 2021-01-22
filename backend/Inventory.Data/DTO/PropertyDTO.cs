namespace Inventory.DataAccess.DTO
{
    public class PropertyDTO
    {
        public int IdProperty { get; private set; }
        public int IdItem { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public PropertyDTO(int idProperty, int idItem, string name, string value)
        {
            IdProperty = idProperty;
            IdItem = idItem;
            Name = name;
            Value = value;
        }
    }
}
