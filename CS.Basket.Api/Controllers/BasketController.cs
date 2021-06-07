using CS.Basket.Api.Applications.Services;
using CS.Basket.Api.Entities;
using CS.Basket.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS.Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : CustomControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            return ResponseOk(basket ?? new ShoppingCart(userName));

        }

        [HttpPost("{userName}", Name ="AddItemsToBasket")]
        public async Task<ActionResult<ShoppingCart>> AddItemsToBasket(string userName,[FromBody] List<ShoppingCartItem> items)
        {
            return ResponseOk(await _basketService.AddItemsToBasket(userName, items));
        }

        [HttpPost("UpdateBasket")]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return ResponseOk(await _basketService.UpdateBasket(basket));
        }


        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketService.DeleteBasket(userName);
            return ResponseOk();
        }
    }
}
