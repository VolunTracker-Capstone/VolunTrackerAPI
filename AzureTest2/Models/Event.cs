using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureTest2.Models
{
    public class Event
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EventID { get; set; }
    public int EventOwnerID { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string EventImage { get; set; } = "string";
    public int VolunteersNeeded { get; set; }
    }
}
