using Employee.Business.Contracts;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private string CurrentUserID => HttpContext.Request.Headers["UserID"].ToString();
        private readonly IEmployeeBusinessService _employeeBusinessService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeBusinessService employeeBusinessService, ILogger<EmployeeController> logger)
        {
            _employeeBusinessService = employeeBusinessService;
            _logger = logger;
        }
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet(Name = "Get")]
        public async Task<IEnumerable<EmployeeEntity>> Get()
        {
            return await _employeeBusinessService.GetAllEmployee();
        }

        [HttpPost(Name = "Update")]
        public async Task Post(EmployeeRequest employeeRequest)
        {
            _employeeBusinessService.TransactionBy = CurrentUserID;
            await _employeeBusinessService.AddOrUpdateEmployee(employeeRequest);
        }
    }
}
