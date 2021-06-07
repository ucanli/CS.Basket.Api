using CS.Basket.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
        Task<ShoppingCart> AddItemsToBasket(string userName, List<ShoppingCartItem> items);
    }
}
