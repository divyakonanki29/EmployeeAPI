using Employee.Business.Contracts;
using Employee.DAL.Contracts;
using Employee.DAL.Implementations;
using Employee.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Implementations
{
    public class EmployeeBusinessService : IEmployeeBusinessService
    {

        public string TransactionBy { get; set; }
        public readonly IEmployeeDataService employeeDataService;
        public EmployeeBusinessService(IEmployeeDataService employeeDataService)
        {
            this.employeeDataService = employeeDataService;
        }
        public async Task AddOrUpdateEmployee(EmployeeRequest employeeRequest)
        {

            DateTime transactionTime = DateTime.UtcNow;
            var dataModel = PrepareDataModel(employeeRequest);
            await employeeDataService.UpsertEmployeeAsync(dataModel);
        }


        public async Task<List<EmployeeEntity>> GetAllEmployee()
        {
            //Get All employees
            var employees = await employeeDataService.GetAllEmployeesWithDependenciesAsync();
            return employees;
        }

        private EmployeeUpdateModel PrepareDataModel(EmployeeRequest employeeRequest)
        {
            var dataModel = new EmployeeUpdateModel();
            string newId = string.Empty;
            if (string.IsNullOrEmpty(employeeRequest.EmployeeId))
            {
                newId = Guid.NewGuid().ToString();
            }


            dataModel.EmployeeId = newId;

            //prepare info
            dataModel.employeeEntity = new EmployeeEntity
            {
                Id = newId,
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                EmployeeType = employeeRequest.EmployeeType,
                CreatedBy = TransactionBy
            };

            //prepare salary

            dataModel.employeeSalaryComponentEntity = new EmployeeSalaryComponentEntity
            {
                EmployeeId = newId,
                AnnualSalary = employeeRequest.EmployeeType != EmployeeTypes.Contract ? employeeRequest.AnnualSalary : 0,
                MaxExpenseAmount = employeeRequest.EmployeeType == EmployeeTypes.Manager ? employeeRequest.MaxExpenseAmount : 0,
                PayPerHour = employeeRequest.EmployeeType == EmployeeTypes.Contract ? employeeRequest.PayPerHour : 0,
                CreatedBy = TransactionBy
            };

            //prepare address
            dataModel.employeeAddressEntity = new AddressEntity
            {
                EmployeeId  = newId,
                AddressLine = employeeRequest.AddressLine,
                CreatedBy = TransactionBy
            };

            return dataModel;
        }
    }
}
