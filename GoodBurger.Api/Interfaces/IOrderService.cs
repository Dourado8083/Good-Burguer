using GoodBurger.Api.Models;

namespace GoodBurger.Api.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllOrder();

    Task<Order> GetOrderById(int id);
    
    Task<Order> CreateOrder(Order novoOrder);

    Task<Order?> UpdateOrder(int id, Order Order);
    Task<bool> DeleteOrder(int id);

}