using System.Collections.Generic;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
    public class Event
    {
    public int EventID { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Street { get; set; }
    public string City { get; set; } 
    public string State { get; set; }
    public string Zip { get; set; }
    public string EventImage { get; set; }
    public int VolunteersNeeded { get; set; }
    }
}
