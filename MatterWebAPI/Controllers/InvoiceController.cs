using MatterWebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using MatterWebAPI.Dto;
[Route("api/[controller]")]
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


    [HttpGet]
    public IActionResult GetAll()
    {
        var invoices = _service.GetAll();
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var invoice = _service.GetById(id);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Create(InvoiceDto invoiceDto)
    {
        _service.Create(invoiceDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, InvoiceDto invoiceDto)
    {
        try
        {
            _service.Update(id, invoiceDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _service.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}

