namespace MatterWebAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }
        public int MatterId { get; set; }
        public int AttorneyId { get; set; }

        public virtual Matter? Matter { get; set; }
        public virtual Attorney? Attorney { get; set; }
    }
}
