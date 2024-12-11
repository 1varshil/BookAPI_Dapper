using BookAPI.Interface;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IDapperService _dapper;

        public BookController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            var books = await _dapper.GetAll();
            if (books == null || !books.Any())
                return NotFound("No books found.");

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            var res = await _dapper.GetBookById(id);
            return res;
        }

        [HttpPost]
        public async Task<Book> Post([FromBody] Book book)
        {
           var task = await _dapper.CreateBook(book);
            return task;
        }

        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] Book book)
        {
            var task = await _dapper.UpdateBook(book);  
            return task;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id, [FromBody] Book book)
        {
            var task = await _dapper.DeleteBook(id);
            return task;
        }

    } 
}
