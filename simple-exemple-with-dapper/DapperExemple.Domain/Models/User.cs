using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace DapperExemple.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Genre { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string MotherName { get; set; }
        public string Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// properties marked with [Computed] attribute, will be ignored by Dapper on insert and update commands
        /// </summary>
        [Computed]
        public Contact Contact { get; set; }

        [Computed]
        public ICollection<Address> Addresses { get; set; }

        [Computed]
        public ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
