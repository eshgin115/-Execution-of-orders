using DemoApplication.Areas.Client.ViewComponents;
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }


        [HttpGet("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Books.Include(b => b.BookImages).FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product);
            if (productsCookieViewModel.Any())
            {
                return ViewComponent(nameof(ShopCart), productsCookieViewModel);
            }

            return ViewComponent(nameof(ShopCart));
        }

        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }


            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts.FirstOrDefaultAsync(p => p.BookId == id);

                _dataContext.BasketProducts.Remove(basketProduct);
                await _dataContext.SaveChangesAsync();
                return ViewComponent(nameof(ShopCart));
            }
            var productCookieValue = HttpContext.Request.Cookies["products"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
            productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

            return ViewComponent(nameof(ShopCart));
        }
    }
}
