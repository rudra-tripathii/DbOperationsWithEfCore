using DbOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCoreApp.Controllers;

[Route("api/currencies")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public CurrencyController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("")] //Get All Data
    public ActionResult GetAllCurrencies()
    {
        var result = _appDbContext.Currencies.ToList();
        //var result = (from currencies in _appDbContext.Currencies
        //              select currencies).ToList();
        return Ok(result);
    }
    //=====================================
    //Get By id
    //=====================================
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAllCurrencyById([FromRoute] int id)
    {
        var result = await _appDbContext.Currencies.FindAsync(id);
        return Ok(result);
    }
    //=====================================
    //Get Currency by name
    //=====================================
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAllCurrencyByName([FromRoute] string name)
    {
        var result = await _appDbContext.Currencies.Where(x => x.Title == name).FirstOrDefaultAsync(); //First Or Default value(Default Provide Null if Value not find)
        var result2 = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync(); //FirsAsync(Provide Exception If Any Value Not Find)
        var result3 = await _appDbContext.Currencies.FirstAsync(x => x.Title == name); //FirsAsync(Provide Exception If Any Value Not Find)

        return Ok(result);
    }

    //=====================================
    // Using Dual Parameters
    //=====================================
    [HttpGet("by-name-description/{name}")]
    public async Task<IActionResult> GetAllCurrencyByNameandDesc([FromRoute] string name, [FromQuery] string? description)
    {
        var result = await _appDbContext.Currencies.FirstAsync(x => x.Title == name && (string.IsNullOrEmpty(description) || x.Description == description)); //FirsAsync(Provide Exception If Any Value Not Find)
        return Ok(result);
    }
    //=======================================
    // Get All Record using Multiple Records
    //=======================================
    [HttpGet("get-all-record-with-para/{name}")]
    public async Task<IActionResult> GetAllRecordsWithParameters([FromRoute] string name, [FromQuery] string? description)
    {
        var result = await _appDbContext.Currencies.Where(x => x.Title == name && (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync();
        return Ok(result);
    }

    [HttpPost("all")]
    public async Task<IActionResult> GetRecordById([FromBody] List<int> ids)
    {
        //Check The List of Ids Are Existance in Table Record
       // var ids = new List<int> {1,4,5,6,7,8,9,8,2};
        var result = await _appDbContext.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync();
        return Ok(result);
    }
    [HttpPost("getSpecificColumn")]
    public async Task<IActionResult> GetSpecficColumn([FromBody] List<int> ids)
    {
       //Option 1
        var result = await _appDbContext.Currencies.
            Where(x => ids.Contains(x.Id))
            .Select(x=>new Currency()
            {
                Id=x.Id,
                Title=x.Title,
            })
            .ToListAsync();

        //Option 2 // Anonymous Object
        var result2 = await _appDbContext.Currencies.
            Where(x => ids.Contains(x.Id))
            .Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
            })
            .ToListAsync();


        return Ok(result);
    }
}
