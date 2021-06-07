using CS.Basket.Api.Entities;
using CS.Basket.Api.Infrastructure.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Applications.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBusControl _busControl;

        public BasketService(IBasketRepository basketRepository, IBusControl busControl)
        {
            _basketRepository = basketRepository;
            _busControl = busControl;
        }


        public async Task<ShoppingCart> GetBasket(string userName)
        {
            return await _basketRepository.GetBasket(userName);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var shoppingChart = await _basketRepository.UpdateBasket(basket);

            await _busControl.Publish(shoppingChart);

            return shoppingChart;
        }

        public async Task<ShoppingCart> AddItemsToBasket(string userName, List<ShoppingCartItem> items)
        {
            var shoppingChart = await _basketRepository.AddItemsToBasket(userName, items);
            await _busControl.Publish(shoppingChart);

            return shoppingChart;
        }

        public async Task DeleteBasket(string userName)
        {
             await _basketRepository.DeleteBasket(userName);
        }
    }
}
