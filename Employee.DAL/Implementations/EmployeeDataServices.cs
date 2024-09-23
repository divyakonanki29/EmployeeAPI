using Employee.DAL.Contracts;
using Employee.DAL.DataFactory;
using Employee.DAL.DBHelper;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DAL.Implementations
{
    public class EmployeeDataServices : IEmployeeDataService
    {

        private readonly EmployeeDBContext _empDbContext;

        public EmployeeDataServices(EmployeeDBContext empDbContext)
        {
            _empDbContext = empDbContext;
        }
        public async Task<List<EmployeeEntity>> GetAllEmployeesAsync()
        {
            return await _empDbContext.Employees.ToListAsync();
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeesWithDependenciesAsync()
        {
            return await _empDbContext.Employees
                .Include(x => x.Address)
                .Include(x => x.EmployeeSalaryComponent)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<EmployeeSalaryComponentEntity>> GetSalaryDetailsByEmployees(List<string> employees)
        {
            return await _empDbContext.EmployeeSalaryComponents.AsQueryable().Where(x=>employees.Contains(x.EmployeeId)).ToListAsync();
        }


        public async Task UpsertEmployeeAsync(EmployeeUpdateModel employeeModel)
        {
            await AddOrUpdateEmployeeAsync(employeeModel);
            
        }

        private async Task AddOrUpdateEmployeeAsync(EmployeeUpdateModel employeeModel)
        {
            using (var transaction = _empDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _empDbContext.UpsertEmployeeInfoAsync(employeeModel.employeeEntity);
                    await _empDbContext.UpsertAddressAsync(employeeModel.employeeAddressEntity);
                    await _empDbContext.UpsertSalaryComponentAsync(employeeModel.employeeSalaryComponentEntity);

                    await _empDbContext.SaveChangesAsync();
                    transaction.Commit(); // Commit the transaction
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Roll back the transaction
                    throw; // this preserves the strack information to the parent calls
                }
            }
        }

    }
}
