using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KaShop1.Models
{
    public class Catgery
    {public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }
        [ValidateNever]

        public List<Proudct> Proudcts { get; set;} = new List<Proudct>();
        
    }
}
