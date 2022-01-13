using api.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IEmployeeRep
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int empId);
        Task<List<string>> GetDeptNamesAsync();
        Task<int> AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int id, Employee employee);
        Task UpdateEmployeeAsyncPatch(int id, JsonPatchDocument employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
