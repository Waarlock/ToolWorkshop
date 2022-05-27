using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class ToolImage
    {
        public int Id { get; set; }

        public Tool Tool{ get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7005/images/noimage.png"
            : $"https://toolorkshop.blob.core.windows.net/products/{ImageId}";

    }
}
