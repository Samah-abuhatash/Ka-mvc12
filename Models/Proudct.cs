namespace KaShop1.Models
{
    public class Proudct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rate {  get; set; }
        public int Quantity {  get; set; }
        public string Image {  get; set; }
        public int categoryId {  get; set; }
        public Catgery category { get; set; }


    }
}
