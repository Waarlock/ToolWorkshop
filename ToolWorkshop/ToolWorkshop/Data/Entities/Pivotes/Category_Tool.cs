using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolWorkshop.Data.Entities
{
    public class Category_Tool
    {
        public int CategoryId { get; set; }
        public int ToolId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("ToolId")]
        public Tool Tool { get; set; }
    }
}
