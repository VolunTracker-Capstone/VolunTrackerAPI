using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureTest2.Models
{
    public class Organization
    { 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int organizationID { get; set; }
    public int OrganizationOwnerID { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string OrganizationImage { get; set; } = "string";
    public string Website { get; set; }
    }
}
