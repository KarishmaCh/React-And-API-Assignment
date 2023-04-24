namespace MatterWebAPI.Models
{
    public class Jurisdiction
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Attorney>? Attorneys { get; set; }
        public virtual ICollection<Matter>? Matters { get; set; }
    }
}
