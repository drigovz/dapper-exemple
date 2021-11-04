using Ecommerce.Domain.Entities;
using FluentValidation;

namespace Ecommerce.Domain.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).Name();
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.RG).NotEmpty();
            RuleFor(x => x.CPF).Identity();
            RuleFor(x => x.MotherName).Name();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
