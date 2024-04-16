using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
    public class Member
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal TotalHours { get; set; } = 0;

    public string ProfilePicture { get; set; } = "string";

    public string Role { get; set; } = "User";
    }
}
