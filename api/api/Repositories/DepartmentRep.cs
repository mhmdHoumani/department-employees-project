using api.Data;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class DepartmentRep : IDepartmentRep
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public DepartmentRep(DatabaseContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> AddDeptAsync(Department depart)
        {
            var newDepart= new Department
            {
                Name = depart.Name
            };
            await _db.Departments.AddAsync(newDepart);
            await _db.SaveChangesAsync();
            return newDepart.DeptID;
        }

        public async Task<bool> DeleteDeptAsync(int id)
        {
            var p = await _db.Departments.FindAsync(id);
            //var b = new Books { Id = id };
            if (p != null)
            {
                _db.Departments.Remove(p);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Department>> GetAllDeptsAsync()
        {
            var departs = await _db.Departments.ToListAsync();
            return _mapper.Map<List<Department>>(departs);
        }

        public async Task<Department> GetDeptByIdAsync(int deptId)
        {
            var depart = await _db.Departments.FindAsync(deptId);
            return _mapper.Map<Department>(depart);
        }

        public async Task UpdateDeptAsync(int id, Department depart)
        {
            var dept = new Department
            {
                DeptID = id,
                Name = depart.Name
            };
            _db.Departments.Update(dept);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateDeptAsyncPatch(int id, JsonPatchDocument depart)
        {
            var p = await _db.Departments.FindAsync(id);
            if (p != null)
            {
                depart.ApplyTo(p);
                await _db.SaveChangesAsync();
            }
        }
    }
}
