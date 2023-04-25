using MatterWebAPI.Dto;
using MatterWebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        private readonly JurisdictionService _jurisdictionService;

        public JurisdictionController(JurisdictionService jurisdictionService)
        {
            _jurisdictionService = jurisdictionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jurisdictions = _jurisdictionService.GetAll();
            return Ok(jurisdictions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jurisdiction = _jurisdictionService.GetById(id);

            // If no jurisdiction found, return NotFound
            if (jurisdiction == null)
            {
                return NotFound();
            }

            return Ok(jurisdiction);
        }

        [HttpPost]
        public IActionResult Create(JurisdictionDTO jurisdictionDTO)
        {
            _jurisdictionService.Create(jurisdictionDTO);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, JurisdictionDTO jurisdictionDTO)
        {
            _jurisdictionService.Update(id, jurisdictionDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _jurisdictionService.Delete(id);
            return NoContent();
        }
    }
}
