using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentRep _departmentRep;
        public DepartmentController(IDepartmentRep departmentRep)
        {
            _departmentRep = departmentRep;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var depts = await _departmentRep.GetAllDeptsAsync();
            return Ok(depts);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            var dept = await _departmentRep.GetDeptByIdAsync(id);
            if (dept == null)
                return NotFound();
            return Ok(dept);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddDepart([FromBody] Department depart)
        {
            var deptId = await _departmentRep.AddDeptAsync(depart);
            depart.DeptID = deptId;
            return CreatedAtAction(nameof(GetDepartmentById), new { id = deptId }, depart);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, [FromBody] Department depart)
        {
            await _departmentRep.UpdateDeptAsync(id, depart);

            return AcceptedAtAction(nameof(GetDepartmentById), new { id }, await _departmentRep.GetDeptByIdAsync(id));
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateDepartPatch([FromRoute] int id, [FromBody] JsonPatchDocument depart)
        {
            await _departmentRep.UpdateDeptAsyncPatch(id, depart);

            return AcceptedAtAction(nameof(GetDepartmentById), new { id }, await _departmentRep.GetDeptByIdAsync(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            if(await _departmentRep.DeleteDeptAsync(id))
                return Ok();
            return NotFound();
        }
    }
}
