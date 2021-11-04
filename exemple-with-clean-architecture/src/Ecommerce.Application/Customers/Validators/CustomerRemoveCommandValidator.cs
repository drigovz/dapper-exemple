using Ecommerce.Application.Customers.Commands;
using FluentValidation;

namespace Ecommerce.Application.Customers.Validators
{
    public class CustomerRemoveCommandValidator : AbstractValidator<CustomerRemoveCommand>
    {
        public CustomerRemoveCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
