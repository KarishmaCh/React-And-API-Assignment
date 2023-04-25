using MatterWebAPI.Dto;
using MatterWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatterWebAPI.Service

{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceDto> GetAll();
        InvoiceDto GetById(int id);
        void Create(InvoiceDto invoiceDto);
        void Update(int id, InvoiceDto invoiceDto);
        void Delete(int id);
    }

    public class InvoiceService : IInvoiceService
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
                Amount = i.HourlyRate * i.HoursWorked,
                HourlyRate = i.HourlyRate,
                HoursWorked = i.HoursWorked
            }).ToList();
        }

        public IEnumerable<InvoiceDto> GetAll()
        {
            var invoices = _dbContext.Invoices.Include(i => i.Matter).Include(i => i.Attorney).ToList();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                HourlyRate = i.HourlyRate,
                HoursWorked = i.HoursWorked,
                MatterId = i.MatterId,
                AttorneyId = i.AttorneyId
            });
        }

        public InvoiceDto GetById(int id)
        {
            var invoice = _dbContext.Invoices.Include(i => i.Matter).Include(i => i.Attorney).SingleOrDefault(i => i.Id == id);

            if (invoice == null) throw new Exception("Invoice not found");

            return new InvoiceDto
            {
                Id = invoice.Id,
                HourlyRate = invoice.HourlyRate,
                HoursWorked = invoice.HoursWorked,
                MatterId = invoice.MatterId,
                AttorneyId = invoice.AttorneyId
            };
        }
        public void Create(InvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                HourlyRate = invoiceDto.HourlyRate,
                HoursWorked = invoiceDto.HoursWorked,
                MatterId = invoiceDto.MatterId,
                AttorneyId = invoiceDto.AttorneyId
            };

            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();
        }

        public void Update(int id, InvoiceDto invoiceDto)
        {
            var invoice = _dbContext.Invoices.SingleOrDefault(i => i.Id == id);

            if (invoice == null) throw new Exception("Invoice not found");

            invoice.HourlyRate = invoiceDto.HourlyRate;
            invoice.HoursWorked = invoiceDto.HoursWorked;
            invoice.MatterId = invoiceDto.MatterId;
            invoice.AttorneyId = invoiceDto.AttorneyId;

            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var invoice = _dbContext.Invoices.SingleOrDefault(i => i.Id == id);

            if (invoice == null) throw new Exception("Invoice not found");

            _dbContext.Invoices.Remove(invoice);
            _dbContext.SaveChanges();
        }


    }
}
