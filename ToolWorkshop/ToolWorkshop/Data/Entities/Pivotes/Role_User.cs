using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolWorkshop.Data.Entities
{
    public class Role_User
    {
        public int RoleId { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
