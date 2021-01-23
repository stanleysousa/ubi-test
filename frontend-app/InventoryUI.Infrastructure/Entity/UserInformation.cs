namespace InventoryUI.Infrastructure.Entity
{
    public class UserInformation
    {
        public Role Role { get; set; }
        public int IdUser { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserInformation(Role role, int idUser, string username, string password)
        {
            Role = role;
            IdUser = idUser;
            Username = username;
            Password = password;
        }
    }
}
