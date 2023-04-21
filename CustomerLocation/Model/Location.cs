namespace Customer.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public int CustomerId { get; set; }
        public virtual CustomerModel Customer { get; set; }

    }

}
