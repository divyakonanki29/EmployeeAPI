using Employee.DAL.DataFactory;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DAL.DBHelper
{
    public static class EmployeeDBHelperExtensions
    {

        public static async Task UpsertAddressAsync(this EmployeeDBContext _empDbContext, AddressEntity employeeAddress)
        {
            if (!string.IsNullOrEmpty(employeeAddress.EmployeeId))
            {
                var existingEmployee = await _empDbContext.Addresses.AsQueryable().Where(x=>x.EmployeeId == employeeAddress.EmployeeId).FirstOrDefaultAsync();
                if (existingEmployee != null)
                {
                    existingEmployee = employeeAddress;
                }
                else
                {
                    await _empDbContext.Addresses.AddAsync(employeeAddress);
                }
            }
        }


        public static async Task UpsertEmployeeInfoAsync(this EmployeeDBContext _empDbContext, EmployeeEntity employee)
        {
            if (!string.IsNullOrEmpty(employee.Id))
            {
                var existingEmployee = await _empDbContext.Employees.FindAsync(employee.Id);
                if (existingEmployee != null)
                {
                    existingEmployee = employee;
                }
                else
                {
                    await _empDbContext.Employees.AddAsync(employee);
                }
            }
        }

        public static async Task UpsertSalaryComponentAsync(this EmployeeDBContext _empDbContext, EmployeeSalaryComponentEntity employeeSalary)
        {
            if (!string.IsNullOrEmpty(employeeSalary.EmployeeId))
            {
                var existingEmployee = await _empDbContext.EmployeeSalaryComponents.AsQueryable().Where(x => x.EmployeeId == employeeSalary.EmployeeId).FirstOrDefaultAsync();
                if (existingEmployee != null)
                {
                    existingEmployee = employeeSalary;
                }
                else
                {
                    await _empDbContext.EmployeeSalaryComponents.AddAsync(employeeSalary);
                }
            }
        }
    }
}
