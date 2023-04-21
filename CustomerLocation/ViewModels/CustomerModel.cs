namespace Customer.Model
{
    public class CustomerDTO
    { 
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
        
        public LocationDTO? location { get; set; }    
    }
    public class LocationDTO
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
