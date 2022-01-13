using api.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IDepartmentRep
    {
        public Task<List<Department>> GetAllDeptsAsync();
        Task<Department> GetDeptByIdAsync(int deptId);
        Task<int> AddDeptAsync(Department depart);
        Task UpdateDeptAsync(int id, Department depart);
        Task UpdateDeptAsyncPatch(int id, JsonPatchDocument depart);
        Task<bool> DeleteDeptAsync(int id);
    }
}
