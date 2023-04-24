using MatterWebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using MatterWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using MatterWebAPI.Dto;

namespace MatterWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MatterController : ControllerBase
    {

        private readonly MatterService _service;

        public MatterController(MatterService service)
        {
            _service = service;
        }
      


        [HttpGet("{clientId:int}/matters")]
        public List<MatterDto> GetMattersByClientId(int clientId)
        {
            return _service.GetMattersByClientId(clientId);
        }

        [HttpGet("{matterId:int}/invoices")]
        public List<Invoice> GetInvoicesByMatterId(int matterId)
        {
            return _service.GetInvoicesByMatterId(matterId);
        }

        [HttpPost("matters")]
        public IActionResult CreateNewMatter([FromBody] MatterDto Matter)
        {
            if (ModelState.IsValid)
            {
                _service.CreateNewMatter(Matter);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /*
        [HttpPost("matters")]
        public IActionResult CreateNewMatter([FromBody] MatterDto Matter)
        {
            if (ModelState.IsValid)
            {
                _service.CreateNewMatter(Matter);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        */

    }
}
