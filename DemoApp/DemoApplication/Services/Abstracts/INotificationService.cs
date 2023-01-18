namespace DemoApplication.Services.Abstracts
{
    public interface INotificationService
    {
        Task SenOrderCreatedToAdmin(string trackingCode);
    }
}
