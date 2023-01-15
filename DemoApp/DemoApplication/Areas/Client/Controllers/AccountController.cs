using DemoApplication.Areas.Client.ViewModels.Account;
using DemoApplication.Contracts.Order;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {
            var user = _userService.CurrentUser;
            var user2 = _userService.CurrentUser;

            return View();
        }

        [HttpGet("orders", Name = "client-account-orders")]
        public async Task<IActionResult> OrdersAsync()
        {
            var model = await _dataContext.Orders
                       .Where(o => o.UserId == _userService.CurrentUser.Id)
                       
                       .Select(o =>
                           new OrderViewModel(
                               o.Id,
                               o.CreatedAt,
                               StatusCodeExtensions.GetShortNameWithStatus(o.Status),
                               o.SumTotalPrice
                              ))
                       .ToListAsync();


            return View(model);
        }
    }
}
