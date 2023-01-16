namespace DemoApplication.Contracts.Order
{
    public enum Status
    {
        Created = 0,
        Confirmed = 1,
        Rejected = 2,
        Submitted = 4,
        Completed = 8,
    }
    public static class StatusCodeExtensions
    {
        public static string GetShortNameWithStatus(this int status)
        {
            switch (status)
            {
                case 0:
                    return "Created";
                case 1:
                    return "Confirmed";
                case 2:
                    return "Rejected";
                case 4:
                    return "Submitted";
                case 8:
                    return "Completed";



                default:
                    throw new Exception("Status  not found");
            }

        }
        public static string GetNotification(this int status,string firstName, string lastName,string order_number)
        {
            switch (status)
            {
                case 0:
                    return  $" Hörmətli {firstName} {lastName}, sizin {order_number} təsdiqləndi.";
           
                case 2:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number}  təsdiqlənmədi.";

                case 4:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number} göndərildi, kuryer sizinlə əlaqə saxlayacaq.";
                case 8:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number} kuryer tərəfindən təhvil verildi.";



                default:
                    throw new Exception("Status  not found");
            }

        }
    }
}
