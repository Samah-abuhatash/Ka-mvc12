using System.ComponentModel.DataAnnotations;

namespace KaShop1.Models
{
    public class Catgery
    {public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }
        public List<Proudct> Proudcts { get; set;} = new List<Proudct>();
        
    }
}
