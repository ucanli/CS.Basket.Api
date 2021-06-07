using CS.Basket.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Applications.Services
{
    public interface IBasketService
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
        Task<ShoppingCart> AddItemsToBasket(string userName, List<ShoppingCartItem> items);
    }
}
