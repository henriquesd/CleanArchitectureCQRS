using CleanArchitecture.Core.Dtos.Book;
using CleanArchitecture.Core.Features.Books.Add;
using CleanArchitecture.Core.Features.Books.Delete;
using CleanArchitecture.Core.Features.Books.Edit;
using CleanArchitecture.Core.Features.Books.Read.GetAllBooks;
using CleanArchitecture.Core.Features.Books.Read.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Web.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : MainController
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllBooksQuery();
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{bookId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById([Required] int bookId)
        {
            try
            {
                var query = new GetBookByIdQuery(bookId);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBook(BookAddDto bookAddDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var command = new AddBookCommand(bookAddDto);

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, BookEditDto bookEditDto)
        {
            try
            {
                if (id != bookEditDto.Id) return BadRequest();

                if (!ModelState.IsValid) return BadRequest();

                var command = new UpdateBookCommand(bookEditDto);

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                var command = new DeleteBookCommand(bookId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}