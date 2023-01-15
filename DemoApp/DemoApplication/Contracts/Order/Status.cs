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
                    throw new Exception("Status size not found");
            }

        }
    }
}
