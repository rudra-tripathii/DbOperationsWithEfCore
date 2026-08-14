using DbOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationsWithEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public ActionResult GetAllLanguage()
        {
            var result = _appDbContext.Languages.ToList();
            
            return Ok(result);
        }
    }
}
