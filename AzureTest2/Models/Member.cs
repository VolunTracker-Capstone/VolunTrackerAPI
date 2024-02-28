namespace AzureTest2.Models;

public class Member
{
    public int MemberID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal TotalHours { get; set; }
    public string ProfilePicture { get; set; }
}