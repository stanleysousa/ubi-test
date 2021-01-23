namespace InventoryUI.DataAccess.DTO
{
    public class UserDTO
    {
        public int IdRole { get; set; }
        public int IdUser { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDTO(int idRole, int idUser, string username, string password)
        {
            IdRole = idRole;
            IdUser = idUser;
            Username = username;
            Password = password;
        }
    }
}
