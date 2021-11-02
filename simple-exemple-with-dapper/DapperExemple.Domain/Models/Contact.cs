namespace DapperExemple.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhoneNumber { get; set; }
        public int CellPhone { get; set; }
        public virtual User User { get; set; }
    }
}
