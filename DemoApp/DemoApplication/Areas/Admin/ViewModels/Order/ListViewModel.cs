namespace DemoApplication.Areas.Admin.ViewModels.Order
{
    public class ListViewModel
    {
        public ListViewModel( string identifikator, DateTime orderDate, string currentStatus, int totalPrice)
        {
            Identifikator = identifikator;
            OrderDate = orderDate;
            CurrentStatus = currentStatus;
            TotalPrice = totalPrice;
        }

        public string Identifikator { get; set; }
        public DateTime OrderDate { get; set; }
        public string CurrentStatus { get; set; }
        public int TotalPrice { get; set; }

      
    }
}
