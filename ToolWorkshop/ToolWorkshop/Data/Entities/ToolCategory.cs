using Microsoft.EntityFrameworkCore;

namespace ToolWorkshop.Data.Entities
{
    public class ToolCategory
    {      
            public int Id { get; set; }

            public Tool Tool { get; set; }

            public Category Category { get; set; }

    }
}
