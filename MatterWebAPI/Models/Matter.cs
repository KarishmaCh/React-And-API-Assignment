namespace MatterWebAPI.Models
{
    public class Matter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? JurisdictionId { get; set; }
        public int? BillingAttorneyId { get; set; }
        public int? ResponsibleAttorneyId { get; set; }
        public int? ClientId { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Jurisdiction Jurisdiction { get; set; }
        public virtual Attorney BillingAttorney { get; set; }
        public virtual Attorney ResponsibleAttorney { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
