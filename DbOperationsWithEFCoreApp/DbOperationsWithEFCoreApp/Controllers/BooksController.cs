using DbOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationsWithEFCoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(AppDbContext appDbContext) : ControllerBase //PrimaryConstructure
{
    [HttpPost]
    public async Task<IActionResult> AddNewBook([FromBody] Book book)
    {
        appDbContext.Books.Add(book);
        await appDbContext.SaveChangesAsync();

        return Ok(book);
    }
    [HttpPost("BulkInsert")]
    public async Task<IActionResult> AddNewBook([FromBody] List<Book> book)
    {
        appDbContext.Books.AddRange(book);
        await appDbContext.SaveChangesAsync();

        return Ok(book);
    }
}
