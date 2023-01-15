using DemoApplication.Areas.Client.ViewModels.Shopping;
using DemoApplication.Attributs;
using DemoApplication.Contracts.Order;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shopping")]
    public class ShoppingController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;


        public ShoppingController(DataContext dbContext, IUserService userService, IOrderService orderService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet("cart", Name = "client-shopping-cart")]
        public IActionResult Cart()
        {
            return View();
        }
        [HttpGet("checkout", Name = "client-shopping-checkout")]
        [ServiceFilter(typeof(IsAuthenticated))]
        public async Task<IActionResult> Checkout()
        {


            var model = new OrderViewModel
            {
                SumTotal = (int)_dbContext.BasketProducts
                  .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Book.Price * bp.Quantity),
                Models = await _dbContext.BasketProducts
                  .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                  .Select(bp =>
                      new OrderViewModel.ItemViewModel(
                          bp.Id,
                          bp.Book.Title,
                          bp.Quantity,
                          bp.Book.Price,
                          bp.Book.Price * bp.Quantity
                          ))
                  .ToListAsync()
            };

            return View(model);

        }
        [HttpPost("placerorder/{id}", Name = "client-shopping-placerorder")]
        public async Task<IActionResult> PlaceOrder([FromRoute] int id)
        {
            var model = new OrderViewModel
            {
                SumTotal = (int)_dbContext.BasketProducts
                  .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Book.Price * bp.Quantity),
                Models = await _dbContext.BasketProducts
                  .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                  .Select(bp =>
                      new OrderViewModel.ItemViewModel(
                          bp.BookId,
                          bp.Book.Title,
                          bp.Quantity,
                          bp.Book.Price,
                          bp.Book.Price * bp.Quantity
                          ))
                  .ToListAsync()
            };

            //await Task.WhenAll
            //    (  _orderService.AddOrderAsync(model.SumTotal),
            //       _orderService.AddOrderProductAsync(model, order.Id)
            //    );

            var order = await _orderService.AddOrderAsync(model.SumTotal);



            await _orderService.AddOrderProductAsync(model, order.Id);

            var pasketProducts = await _dbContext.BasketProducts
                        .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                       .ToListAsync();

             _dbContext.BasketProducts.RemoveRange(pasketProducts);


            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-account-orders");

        }
    }
}
