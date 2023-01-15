namespace DemoApplication.Areas.Client.ViewModels.Shopping
{
    public class OrderViewModel
    {
    

        public int SumTotal { get; set; }
        public List<ItemViewModel> Models { get; set; }
        public class ItemViewModel
        {
            public ItemViewModel(int id, string? title, int quantity, decimal price, decimal total)
            {
                Id = id;
                Title = title;
                Quantity = quantity;
                Price = price;
                Total = total;
            }

            public int Id { get; set; }
            public string? Title { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}
