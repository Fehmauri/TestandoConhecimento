using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestandoConhecimento.API.Entities;
using TestandoConhecimento.API.Percistence;

namespace TestandoConhecimento.API.Controllers
{
    [Route("api/testando-conhecimento")]
    [ApiController]
    public class TestandoController : ControllerBase
    {
        private readonly ConhecimentoDbContext _context;

        public TestandoController(ConhecimentoDbContext context) 
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var conhecimento = _context.Conheciementos.Where(x => !x.IsDeleted).ToList();

            return Ok(conhecimento);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var conhecimento = _context.Conheciementos.SingleOrDefault(x => x.Id == id);

            if(conhecimento == null)
                return NotFound();

            return Ok(conhecimento);
        }

        [HttpPost]
        public IActionResult Post(Conheciemento conhecimento)
        {
            _context.Conheciementos.Add(conhecimento);

            return CreatedAtAction(nameof(GetById), new { id = conhecimento.Id }, conhecimento);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Conheciemento input)
        {
            var conhecimento = _context.Conheciementos.SingleOrDefault(x => x.Id == id);

            if (conhecimento == null)
                return NotFound();

            conhecimento.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var conhecimento = _context.Conheciementos.SingleOrDefault(x => x.Id == id);

            if (conhecimento == null)
                return NotFound();

            conhecimento.Delete();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, ConhecimentoSpeaker speaker)
        {
            var conhecimento = _context.Conheciementos.SingleOrDefault(x => x.Id == id);

            if (conhecimento == null)
                return NotFound();

            conhecimento.Speakers.Add(speaker);

            return NoContent();
        }
    }
}
