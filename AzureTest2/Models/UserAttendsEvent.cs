namespace AzureTest2.Models
{
    public class UserAttendsEvent
    {
    public int MemberID { get; set; }
    public int EventID { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    }
}
