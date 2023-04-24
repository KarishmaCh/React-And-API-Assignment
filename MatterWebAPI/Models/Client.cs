namespace MatterWebAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Matter>? Matters { get; set; }
    }
}
