using AutoFixture.Xunit2;
using CS.Basket.Api.Applications.Services;
using CS.Basket.Api.Entities;
using CS.Basket.Api.Infrastructure.Repositories;
using CS.Basket.Api.Test.Fixtures;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CS.Basket.Api.Test.Application.Services
{
    [Trait("Service", "BasketService")]
    public class BasketServiceTest : IClassFixture<BasketServiceFixture>
    {
        private readonly BasketService _sut;
        private readonly BasketServiceFixture _fixtureContainer;

        private readonly Mock<IBasketRepository> _mockedBasketRepository;

        public BasketServiceTest(BasketServiceFixture fixtureContainer)
        {
            _fixtureContainer = fixtureContainer;
            _sut = fixtureContainer.BasketService;
            _mockedBasketRepository = fixtureContainer.MockedBasketRepository;
        }


        [Theory]
        [AutoData]
        public async Task GetBasket(string userName)
        {
            var response = await _sut.GetBasket(userName);

            Assert.NotNull(response);
        }


        [AutoData]
        [Theory]
        public async Task GetBasket_Response(string userName, ShoppingCart basket)
        {
            _mockedBasketRepository.Setup(x => x.GetBasket(It.IsAny<string>())).ReturnsAsync(basket);

            var result = await _sut.GetBasket(userName);

            Assert.Equal(basket, result);
        }

        [AutoData]
        [Theory]
        public async Task AddItemsToBasket(string userName, List<ShoppingCartItem> items)
        {
            var result = await _sut.AddItemsToBasket(userName, items);

            Assert.NotNull(result);
        }

        [AutoData]
        [Theory]
        public async Task UpdateBasket(ShoppingCart basket)
        {
            var result = await _sut.UpdateBasket(basket);

            Assert.NotNull(result);
        }

        [AutoData]
        [Theory]
        public async Task DeleteBasket(string userName)
        {
            try
            {
                await _sut.DeleteBasket(userName);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
