using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Companies.API.Controllers
{
    [Route("api/demo")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public DemoController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok("Working");
        }

        [HttpGet("dto")]
        public ActionResult Index2()
        {
            var dto = new CompanyDto { Name = "Working" };
            return Ok(dto);
        }

        [HttpGet("getone")]
        public async Task<ActionResult> Get()
        {
            var dto = (await serviceManager.CompanyService.GetAsync(true)).First();
            return Ok(dto);
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var dtos = await serviceManager.CompanyService.GetAsync(false);
            return Ok(dtos);
        }
    }
}
