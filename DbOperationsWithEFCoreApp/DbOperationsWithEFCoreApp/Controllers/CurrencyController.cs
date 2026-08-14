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

    [HttpGet("")]
    public ActionResult GetAllCurrencies()
    {
         var result = _appDbContext.Currencies.ToList();
        //var result = (from currencies in _appDbContext.Currencies
        //              select currencies).ToList();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAllCurrencyById([FromRoute] int id)
    {
        var result = await _appDbContext.Currencies.FindAsync(id);
        //var result = (from currencies in _appDbContext.Currencies
        //              select currencies).ToList();
        return Ok(result);
    }
}
