
using CS.Basket.Api.Applications.Services;
using CS.Basket.Api.Controllers;
using CS.Basket.Api.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq;
using Xunit;
using AutoFixture.Xunit2;
using System.Threading.Tasks;
using System.Collections.Generic;
using CS.Basket.Api.Entities;

namespace CS.Basket.Api.Test.Controllers
{
    [Trait("Controller", "BasketController")]
    public class BasketControllerTest
    {
        private readonly BasketController _basketController;
        private readonly Mock<IOptions<AppSettings>> _mockAppSettings;
        private readonly Mock<IBasketService> _mockBasketService;

        public BasketControllerTest()
        {
            _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockBasketService = new Mock<IBasketService>();

            _basketController = new BasketController(_mockBasketService.Object);
        }

        [AutoData]
        [Theory]
        public async Task GetBasket(string userName)
        {
            var result = await _basketController.GetBasket(userName);

            Assert.NotNull(result);
        }

        [AutoData]
        [Theory]
        public async Task AddItemsToBasket(string userName, List<ShoppingCartItem> items)
        {
            var result = await _basketController.AddItemsToBasket(userName, items);

            Assert.NotNull(result);
        }

        [AutoData]
        [Theory]
        public async Task UpdateBasket(ShoppingCart basket)
        {
            var result = await _basketController.UpdateBasket(basket);

            Assert.NotNull(result);
        }

        [AutoData]
        [Theory]
        public async Task DeleteBasket(string userName)
        {
            var result = await _basketController.DeleteBasket(userName);

            Assert.NotNull(result);
        }
    }
}
