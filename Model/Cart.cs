namespace TruYumWebAPI.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public string Item { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Userid { get; set; }
        public int TotalPrice { get; set; }
    }
}
