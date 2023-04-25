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
        [HttpPost]
        public IActionResult CreateNewMatter([FromBody] MatterDto matterDto)
        {
            if (matterDto == null)
            {
                return BadRequest();
            }

            _service.CreateNewMatter(matterDto);

            return CreatedAtAction(nameof(GetAllMatters), new { }, matterDto);
        }
        [HttpGet]
        public IActionResult GetAllMatters()
        {
            var matters = _service.GetAllMatters();

            return Ok(matters);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatter(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _service.DeleteMatter(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateMatter(int id, [FromBody] MatterDto matterDto)
        {
            if (matterDto == null || id <= 0)
            {
                return BadRequest();
            }

            _service.UpdateMatter(id, matterDto);

            return NoContent();
        }




    }
}
