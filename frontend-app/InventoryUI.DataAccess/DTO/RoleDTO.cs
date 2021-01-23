namespace InventoryUI.DataAccess.DTO
{
    public class RoleDTO
    {
        public int IdRole { get; private set; }
        public string RoleDescription { get; set; }

        public RoleDTO(int idUser, string username)
        {
            IdRole = idUser;
            RoleDescription = username;
        }
    }
}
