namespace DemoApplication.Areas.Admin.ViewModels.BookImage
{
    public class UpdateViewModel
    {
        public string? ImageUrL { get; set; }
        public int? Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}
