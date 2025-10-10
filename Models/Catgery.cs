namespace KaShop1.Models
{
    public class Catgery
    {public int Id { get; set; }

        public string Name { get; set; }
        public List<Proudct> Proudcts { get; set;} = new List<Proudct>();
        
    }
}
