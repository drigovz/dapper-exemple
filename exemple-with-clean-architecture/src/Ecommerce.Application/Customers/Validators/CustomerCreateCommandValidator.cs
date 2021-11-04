using Ecommerce.Application.Customers.Commands;
using FluentValidation;
using Ecommerce.Domain.Validations;

namespace Ecommerce.Application.Customers.Validators
{
    public class CustomerCreateCommandValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateCommandValidator()
        {
            RuleFor(x => x.Name).Name();
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Genre).NotNull().NotEmpty();
            RuleFor(x => x.RG).NotNull().NotEmpty();
            RuleFor(x => x.CPF).Identity();
            RuleFor(x => x.MotherName).NotNull().NotEmpty();
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }
}
