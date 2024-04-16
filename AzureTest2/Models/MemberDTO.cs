namespace AzureTest2.Models
{
    public class MemberDTO //data transfer object
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal TotalHours { get; set; }
        public string Role { get; set; } = "User";
    }
}
