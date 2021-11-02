namespace DapperExemple.Domain.Models
{
    public class UserDepartment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public virtual User Users { get; set; }
        public virtual Department Departments { get; set; }
    }
}
