using MatterWebAPI.Dto;
using MatterWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatterWebAPI.Interface;

namespace MatterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttorneyController : ControllerBase
    {
        private readonly IAttorneyService _attorneyService;

        public AttorneyController(IAttorneyService attorneyService)
        {
            _attorneyService = attorneyService;
        }

        [HttpGet]
        public IActionResult GetAttorneys()
        {
            var attorneys = _attorneyService.GetAttorneys();

            return Ok(attorneys);
        }

        [HttpGet("{id}")]
        public IActionResult GetAttorneyById(int id)
        {
            var attorney = _attorneyService.GetAttorneyById(id);

            if (attorney == null)
            {
                return NotFound();
            }

            return Ok(attorney);
        }

        [HttpPost]
        public IActionResult CreateAttorney(AttorneyDTO attorneyDto)
        {
            if (attorneyDto == null)
            {
                return BadRequest();
            }

            _attorneyService.CreateAttorney(attorneyDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttorney(int id, AttorneyDTO attorneyDto)
        {
            if (attorneyDto == null)
            {
                return BadRequest();
            }

            _attorneyService.UpdateAttorney(id, attorneyDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttorney(int id)
        {
            _attorneyService.DeleteAttorney(id);

            return Ok();
        }
    }
}