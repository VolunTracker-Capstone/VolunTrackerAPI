using System.Collections.Generic;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
    public class Organization
    {
    public int organizationID { get; set; }
    public string orgName { get; set; }
    public string orgAddress { get; set; }
    public string orgWebsite { get; set; }
    }
}
