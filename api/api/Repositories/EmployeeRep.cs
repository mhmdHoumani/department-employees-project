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
    public class EmployeeRep : IEmployeeRep
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public EmployeeRep(DatabaseContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            var newEmployee = new Employee
            {
                Name = employee.Name,
                Department = employee.Department,
                StartDate = employee.StartDate,
                PhotoName = employee.PhotoName
            };
            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
            return newEmployee.EmpID;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            //var b = new Books { Id = id };
            if (e != null)
            {
                _db.Employees.Remove(e);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _db.Employees.ToListAsync();
            return _mapper.Map<List<Employee>>(employees);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            var employee = await _db.Employees.FindAsync(empId);
            return _mapper.Map<Employee>(employee);
        }
        
        public async Task<List<string>> GetDeptNamesAsync()
        {
            return await _db.Departments.Select(dep => dep.Name).ToListAsync();
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            var emp = new Employee
            {
                EmpID = id,
                Name = employee.Name,
                Department = employee.Department,
                PhotoName = employee.PhotoName,
                StartDate = employee.StartDate
            };
            _db.Employees.Update(emp);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsyncPatch(int id, JsonPatchDocument employee)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e != null)
            {
                employee.ApplyTo(e);
                await _db.SaveChangesAsync();
            }
        }
    }
}
