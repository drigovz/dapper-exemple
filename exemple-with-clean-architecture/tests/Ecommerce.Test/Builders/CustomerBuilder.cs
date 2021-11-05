using Ecommerce.Domain.Entities;
using Bogus;

namespace Ecommerce.Test.Builders
{
    public class CustomerBuilder
    {
        private static Faker faker = new Faker("en");

        private string Name = faker.Person.FullName;
        private string Email = faker.Internet.Email();
        private string Genre = "F";
        private string RG = "0000000000";
        private string CPF = "00000000000";
        private string MotherName = faker.Name.FullName();
        private string Status = "A";

        public static CustomerBuilder New()
        {
            return new CustomerBuilder();
        }

        public CustomerBuilder CustomerWithName(string name)
        {
            Name = name;
            return this;
        }

        public CustomerBuilder CustomerWithEmail(string email)
        {
            Email = email;
            return this;
        }

        public CustomerBuilder CustomerWithGenre(string genre)
        {
            Genre = genre;
            return this;
        }

        public CustomerBuilder CustomerWithRg(string rg)
        {
            RG = rg;
            return this;
        }

        public CustomerBuilder CustomerWithCpf(string cpf)
        {
            CPF = cpf;
            return this;
        }

        public CustomerBuilder CustomerWithMotherName(string motherName)
        {
            MotherName = motherName;
            return this;
        }

        public CustomerBuilder CustomerWithMotherStatus(string status)
        {
            Status = status;
            return this;
        }

        public Customer Build() =>
            new Customer(Name, Email, Genre, RG, CPF, MotherName, Status);
    }
}
