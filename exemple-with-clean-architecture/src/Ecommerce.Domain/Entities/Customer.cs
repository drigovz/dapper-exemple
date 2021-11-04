using Dapper.Contrib.Extensions;
using Ecommerce.Domain.Validations;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Genre { get; private set; }
        public string RG { get; private set; }
        public string CPF { get; private set; }
        public string MotherName { get; private set; }
        public string Status { get; private set; }

        [Computed]
        public Contact Contact { get; set; }
        [Computed]
        public ICollection<Address> Addresses { get; set; }
        [Computed]
        public ICollection<UserDepartment> UserDepartments { get; set; }

        public Customer()
        { }

        public Customer(string name, string email, string genre, string rg, string cpf, string motherName, string status)
        {
            Name = name;
            Email = email;
            Genre = genre;
            RG = rg;
            CPF = cpf;
            MotherName = motherName;
            Status = status;

            EntityValidation(this, new CustomerValidator());
        }

        public Customer(int id, string name, string email, string genre, string rg, string cpf, string motherName, string status)
        {
            Id = id;

            EntityValidation(this, new CustomerValidator());
        }

        public Customer Update(string name, string email, string genre, string rg, string cpf, string motherName, string status)
        {
            Name = name;
            Email = email;
            Genre = genre;
            RG = rg;
            CPF = cpf;
            MotherName = motherName;
            Status = status;

            EntityValidation(this, new CustomerValidator());
            return this;
        }
    }
}
