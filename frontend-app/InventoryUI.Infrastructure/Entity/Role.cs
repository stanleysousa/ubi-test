namespace InventoryUI.Infrastructure.Entity
{
    public class Role
    {
        public int IdRole { get; private set; }
        public string RoleDescription { get; set; }

        public Role(int idRole, string roleDescription)
        {
            IdRole = idRole;
            RoleDescription = roleDescription;
        }
    }
}
