using AutoFixture;
using AutoFixture.AutoMoq;
using CS.Basket.Api.Applications.Services;
using CS.Basket.Api.Infrastructure.Repositories;
using CS.Basket.Api.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Basket.Api.Test.Fixtures
{
    public class BasketServiceFixture
    {
        public IFixture Fixture { get; set; }
        public BasketService BasketService { get; set; }
        public Mock<IOptions<AppSettings>> MockedAppSettings { get; set; }
        public Mock<IBasketRepository> MockedBasketRepository { get; set; }
        public Mock<IBusControl> MockedBusControl { get; set; }
        public Mock<BasketService> MockedBasketService { get; set; }

        public BasketServiceFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());

            this.MockedAppSettings = Fixture.Freeze<Mock<IOptions<AppSettings>>>();
            this.MockedBasketRepository = Fixture.Freeze<Mock<IBasketRepository>>();
            this.MockedBusControl = Fixture.Freeze<Mock<IBusControl>>();
            this.MockedBasketService = Fixture.Freeze<Mock<BasketService>>();

            this.BasketService = MockedBasketService.Object;
        }
    }
}
