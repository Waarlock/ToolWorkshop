using ToolWorkshop.Common;
using ToolWorkshop.Data.Entities;

namespace ToolWorkshop.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Tool> Tools{ get; set; }

        public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }

    }
}
