using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Entities.Validators
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(s => s.UserName).NotNull().NotEmpty();
            RuleFor(s => s.Items).NotNull().NotEmpty();

        }
    }
}
