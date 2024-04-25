namespace AzureTest2.Models
{
    public class OrganizationCreate
    {
        public int OrganizationOwnerID { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
    }
}