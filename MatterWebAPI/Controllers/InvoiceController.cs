using MatterWebAPI.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _service;

    public InvoiceController(InvoiceService service)
    {
        _service = service;
    }

    [HttpGet("api/invoices/billing/{attorneyId}")]
    public decimal GetLastWeeksBillingForAttorney(int attorneyId)
    {
        return _service.GetLastWeeksBillingForAttorney(attorneyId);
    }
}
