using System.Collections.Generic;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
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
    
    public static List<Member> GetMembers(MyAzureContext context)
    {
        return context.Member.ToList();
    }

    public static Member GetMemberById(MyAzureContext context, int id)
    {
        return context.Member.Find(id);
    }

    public static void AddMember(MyAzureContext context, Member member)
    {
        context.Member.Add(member);
        context.SaveChanges();
    }

    // Add other methods for updating, deleting, or querying members
    }
}
