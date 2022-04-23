namespace ToolWorkshop.Data.Entities
{
    public class Role_User
    {
        public int Id { get; set; }

        public Role RoleId { get; set; }

        public User UserId { get; set; }
    }
}
