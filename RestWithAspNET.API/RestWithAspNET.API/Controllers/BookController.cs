using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNET.API.Business.Implementations;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Hypermedia.Filters;
using RestWithAspNET.API.Model;
using RestWithAspNET.API.Repository;

namespace RestWithAspNET.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookBusiness _bookRepository;

        public BookController(IBookBusiness bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookRepository.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var Book = _bookRepository.FindById(id);

            if (Book == null) return NotFound();

            return Ok(Book);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO Book)
        {
            if (Book == null) return BadRequest();

            return Ok(_bookRepository.Create(Book));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO Book)
        {
            if (Book == null) return BadRequest();

            return Ok(_bookRepository.Update(Book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookRepository.Delete(id);

            return NoContent();
        }

    }
}
