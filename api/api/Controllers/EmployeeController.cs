using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRep _employeeRep;
        
        public EmployeeController(IEmployeeRep employeeRep, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRep = employeeRep;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRep.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employee = await _employeeRep.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var empId = await _employeeRep.AddEmployeeAsync(employee);
            employee.EmpID = empId;
            return CreatedAtAction(nameof(GetEmployeeById), new { id = empId }, employee);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            await _employeeRep.UpdateEmployeeAsync(id, employee);

            return AcceptedAtAction(nameof(GetEmployeeById), new { id }, await _employeeRep.GetEmployeeByIdAsync(id));
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateEmplpoyeePatch([FromRoute] int id, [FromBody] JsonPatchDocument employee)
        {
            await _employeeRep.UpdateEmployeeAsyncPatch(id, employee);

            return AcceptedAtAction(nameof(GetEmployeeById), new { id }, await _employeeRep.GetEmployeeByIdAsync(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if(await _employeeRep.DeleteEmployeeAsync(id))
                return Ok();
            return NotFound();
        }
        [HttpGet("GetAllDepartmentsNames")]
        public async Task<IActionResult> GetAllDepartmentsNames()
        {
            return Ok(await _employeeRep.GetDeptNamesAsync());
        }
        [Produces("Application/json")]
        [HttpPost("upload")]
        public IActionResult SaveFile(IFormFile file)
        {
            //var baseURL = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.PathBase;
            try
            {
                string imgExt = Path.GetExtension(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", file.FileName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".png"))
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    return Ok(new { fileName = file.FileName});
                    //return Ok(new { fileName = baseURL + "/upload/" + file.FileName});
                }
                return Ok(new { fileName = "Invalid" });
            }
            catch (Exception)
            {
                return BadRequest(new { fileName = "unknown.png" });
                //return BadRequest(new { fileName = baseURL + "/upload/unknown.png" });
            }
        }
    }
}
