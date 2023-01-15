using DemoApplication.Areas.Admin.ViewModels.Order;
using DemoApplication.Contracts.Order;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/order")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public OrderController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dbContext.Orders


                   .Select(o =>
                       new ListViewModel(
                           o.Id,
                           o.CreatedAt,
                           StatusCodeExtensions.GetShortNameWithStatus(o.Status),
                           o.SumTotalPrice
                          ))
                   .ToListAsync();

            return View();
        }
        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync(string id)
        {
            var order = await _dbContext.Orders.Select(o => o.Id == id).ToListAsync();
            if (order is null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost("delete/{id}", Name = "admin-order-delete")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
