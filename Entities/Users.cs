namespace WebApplication2.Entities
{
    public class Users
    {
        public int Id { get; set; } // Primary Key
        public string user_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
