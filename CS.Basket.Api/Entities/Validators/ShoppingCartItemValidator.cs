using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Entities.Validators
{
    public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
    {
        public ShoppingCartItemValidator()
        {
            RuleFor(s => s.Quantity).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(s => s.ProductId).NotNull().NotEmpty().GreaterThan(0);

        }
    }
}
