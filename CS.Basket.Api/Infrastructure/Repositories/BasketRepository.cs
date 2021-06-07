using CS.Basket.Api.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _database.StringGetAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _database.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task<ShoppingCart> AddItemsToBasket(string userName, List<ShoppingCartItem> items)
        {
            var basketByUser = await GetBasket(userName);

            if (basketByUser != null)
            {
                basketByUser.Items.AddRange(items);
                await _database.StringSetAsync(userName, JsonConvert.SerializeObject(basketByUser));
            }
            else
            {
                var shoppingChart = new ShoppingCart(userName);
                shoppingChart.Items = items;
                await _database.StringSetAsync(userName, JsonConvert.SerializeObject(shoppingChart));
            }

            return await GetBasket(userName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _database.KeyDeleteAsync(userName);
        }
    }
}
