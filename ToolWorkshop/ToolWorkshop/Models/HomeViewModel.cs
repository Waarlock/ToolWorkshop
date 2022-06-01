using ToolWorkshop.Data.Entities;

namespace ToolWorkshop.Models
{
    public class HomeViewModel
    {
        public ICollection<Tool> Tools { get; set; }

        public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }

    }
}
