using MatterWebAPI.Dto;

namespace MatterWebAPI.Service

{
    public class InvoiceService
    {
        private readonly AppDbContext _dbContext;
        public InvoiceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetLastWeeksBillingForAttorney(int attorneyId)
        {
            var lastWeekStart = DateTime.Now.AddDays(-7).Date;
            var lastWeekEnd = DateTime.Now.Date;

            return _dbContext.Invoices
                .Where(i => i.AttorneyId == attorneyId &&
                    i.Matter.CreatedOn >= lastWeekStart && i.Matter.CreatedOn <= lastWeekEnd)
                .Join(_dbContext.Matters, i => i.MatterId, m => m.Id, (i, m) => new { Invoice = i, Matter = m })
                .Sum(im => im.Invoice.HourlyRate * im.Invoice.HoursWorked);
        }

        public List<InvoiceDto> GetInvoicesByMatterId(int matterId)
        {
            return _dbContext.Invoices.Where(i => i.MatterId == matterId)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    Amount = i.HourlyRate*i.HoursWorked,
                    HourlyRate = i.HourlyRate,
                    HoursWorked = i.HoursWorked
                }).ToList();
        }
    }
}
