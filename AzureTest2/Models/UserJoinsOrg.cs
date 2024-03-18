using System.Collections.Generic;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
    public class UserJoinsOrg
    {
    public int MemberID { get; set; }
    public int OrgID { get; set; }
    public double HoursWorked { get; set; }
    public string Role { get; set; }
    public DateTime JoinDate { get; set; }
    public int TagID { get; set; }
    }
}
