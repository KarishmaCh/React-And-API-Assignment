namespace MatterWebAPI.Models
{
    public class Attorney
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public int JurisdictionId { get; set; }

        public virtual Jurisdiction? Jurisdiction { get; set; }
    }
}
