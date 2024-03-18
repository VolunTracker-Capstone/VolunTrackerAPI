using System.Collections.Generic;
using System.Linq;
using AzureTest2.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Models
{
    public class TagsList
    {
    public int TagID { get; set; }
    public int OrgID { get; set; }
    public string TagTitle { get; set; }
    }
}
