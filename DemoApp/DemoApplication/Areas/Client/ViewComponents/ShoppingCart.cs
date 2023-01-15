using DemoApplication.Areas.Client.ViewModels.Shopping;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoApplication.Areas.Client.ViewComponents
{
    public class ShoppingCart : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public ShoppingCart(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Case 1 : Qeydiyyat kecilib, o zaman bazadan gotur
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                    .Select(bp =>
                        new ProductCookieViewModel(
                            bp.BookId,
                            bp.Book!.Title,
                            bp.Book.BookImages.Take(1)!.FirstOrDefault()! != null
                        ? _fileService.GetFileUrl(bp.Book.BookImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Book)
                        : string.Empty,
                            bp.Quantity,
                            bp.Book.Price,
                            bp.Book.Price * bp.Quantity))
                    .ToListAsync();

                return View(model);
            }


            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
