using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models.Products
{
    public class ProductViewModel
    {
        public required int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public required string Description { get; set; }

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1 , 100000)]
        public decimal Price { get; set; }

        public string? PictureUrl { get; set; }



        public ProductCategory Category { get; set; } = null!;
        public ProductBrand Brand { get; set; } = null!;

    }
}
