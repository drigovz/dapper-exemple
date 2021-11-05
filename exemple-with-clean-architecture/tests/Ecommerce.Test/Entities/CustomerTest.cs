using Ecommerce.Test.Builders;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Test.Entities
{
    public class CustomerTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Customers")]
        public void Should_Not_Create_Customer_With_Invalid_Name(string name)
        {
            var customer = CustomerBuilder.New().CustomerWithName(name).Build();

            customer.Valid.Should().BeFalse();
            customer.ValidationResult.Errors.Should().NotBeEmpty();
            customer.ValidationResult.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            customer.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage.Contains("Name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("exemple")]
        [InlineData("exemple@")]
        [InlineData("@exemple")]
        [Trait("Domain", "Customers")]
        public void Should_Not_Create_Customer_With_Invalid_Email(string invalidEmail)
        {
            var customer = CustomerBuilder.New().CustomerWithEmail(invalidEmail).Build();

            customer.Valid.Should().BeFalse();
            customer.ValidationResult.Errors.Should().NotBeEmpty();
            customer.ValidationResult.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            customer.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage.Contains("Email"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Customers")]
        public void Should_Not_Create_Customer_With_Invalid_Genre(string genre)
        {
            var customer = CustomerBuilder.New().CustomerWithGenre(genre).Build();

            customer.Valid.Should().BeFalse();
            customer.ValidationResult.Errors.Should().NotBeEmpty();
            customer.ValidationResult.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            customer.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage.Contains("Genre"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Customers")]
        public void Should_Not_Create_Customer_With_Invalid_Rg(string rg)
        {
            var customer = CustomerBuilder.New().CustomerWithRg(rg).Build();

            customer.Valid.Should().BeFalse();
            customer.ValidationResult.Errors.Should().NotBeEmpty();
            customer.ValidationResult.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            customer.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage.Contains("RG"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("307")]
        [InlineData("30700")]
        [InlineData("3070069")]
        [InlineData("307006960")]
        [InlineData("3070069606")]
        [Trait("Domain", "Customers")]
        public void Should_Not_Create_Customer_With_Invalid_Cpf(string cpf)
        {
            var customer = CustomerBuilder.New().CustomerWithCpf(cpf).Build();

            customer.Valid.Should().BeFalse();
            customer.ValidationResult.Errors.Should().NotBeEmpty();
            customer.ValidationResult.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            customer.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage.Contains("Identity"));
        }
    }
}
