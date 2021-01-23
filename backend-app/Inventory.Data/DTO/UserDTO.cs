namespace Inventory.DataAccess.DTO
{
    public class UserDTO
    {
        public int IdUser { get; private set; }
        public string Username { get; set; }

        public UserDTO(int idUser, string username)
        {
            IdUser = idUser;
            Username = username;
        }
    }
}
