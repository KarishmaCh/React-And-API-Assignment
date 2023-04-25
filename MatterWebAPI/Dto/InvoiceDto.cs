using MatterWebAPI.Models;

namespace MatterWebAPI.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }

        public int MatterId { get; set; }
        public int AttorneyId { get; set; }

      


    }
}
