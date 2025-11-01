using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KaShop1.Models
{
    public class Proudct
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "product name is required.")]

        [MinLength(3, ErrorMessage = "product name must be at least 3 char long")]

        [MaxLength(100, ErrorMessage = "product name con't axceed 100 char")]

        public string Name { get; set; }
        [MinLength(20, ErrorMessage = "product description must be at least 20 char long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "product price is required.")]

        [Range(0.01, 10000, ErrorMessage = "price must be between 0.01 and 10000")]

        public decimal Price { get; set; }
        public int Rate { get; set; }
        [Required(ErrorMessage = "product quantity is required.")]

        [Range(1, int.MaxValue)]

        public int Quantity { get; set; }
        [ValidateNever]

        public string Image { get; set; }
        public int categoryId { get; set; }
        [ValidateNever]
        public Catgery category { get; set; }


    }
}