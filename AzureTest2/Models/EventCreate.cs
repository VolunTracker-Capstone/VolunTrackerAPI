namespace AzureTest2.Models
{
    public class EventCreate
    {
        public int EventOwnerID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public string City { get; set; } 
        public string State { get; set; }
        public string Zip { get; set; }
        public int VolunteersNeeded { get; set; }
    }
}