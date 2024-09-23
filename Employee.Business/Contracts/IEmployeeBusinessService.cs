using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Models;

namespace Employee.Business.Contracts
{
    public interface IEmployeeBusinessService
    {

        public string TransactionBy { get; set; }
        Task<List<EmployeeEntity>> GetAllEmployee();
        Task AddOrUpdateEmployee(EmployeeRequest employeeRequest);
    }
}
