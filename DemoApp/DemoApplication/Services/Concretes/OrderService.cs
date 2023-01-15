using DemoApplication.Areas.Client.ViewModels.Shopping;
using DemoApplication.Contracts.Order;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;

namespace DemoApplication.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private const int MIN_RANDOM_NUMBER = 10000;
        private const int MAX_RANDOM_NUMBER = 100000;
        private const string PREFIX = "OR";


        public OrderService(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }
        public async Task<Order> AddOrderAsync(int SumToal)
        {
            ArgumentNullException.ThrowIfNull(SumToal);
            
 
            var order = new Order
            {
                Id = GenerateId(),
                SumTotalPrice = SumToal,
                User = _userService.CurrentUser,
                Status = (int)Status.Created,

            };
            await _dataContext.Orders.AddAsync(order);
            return order;
        }
  

        public async Task AddOrderProductAsync(OrderViewModel model,string OrderId)
        {
            ArgumentNullException.ThrowIfNull(model);
            foreach (var item in model.Models)
            {

                var orderProduct = new OrderProduct
                {
                    BookId = item.Id,
                    Orderİd = OrderId,
                    Quantity = item.Quantity,
                };
                await _dataContext.orderProducts.AddAsync(orderProduct);
            }
        }
        private string GenerateNumber()
        {
            Random random = new Random();
            return random.Next(MIN_RANDOM_NUMBER, MAX_RANDOM_NUMBER).ToString();
        }

        private string GenerateId()
        {
            var Id = string.Empty;
            while (true)
            {
                Id = $"{PREFIX}{GenerateNumber()}";

                if (!_dataContext.Orders.Any(o => o.Id == Id))
                {
                    return Id;
                }
            }
        }
    }
}
