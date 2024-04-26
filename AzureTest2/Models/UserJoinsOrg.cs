namespace AzureTest2.Models
{
    public class UserJoinsOrg
    {
    public int MemberID { get; set; }
    public int OrgID { get; set; }
    public Decimal HoursWorked { get; set; }
    public string Role { get; set; }
    public DateTime JoinDate { get; set; }
    public int TagID { get; set; } = 0;
    }
}
