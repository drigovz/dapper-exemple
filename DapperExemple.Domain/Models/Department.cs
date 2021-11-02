using System.Collections.Generic;

namespace DapperExemple.Domain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
