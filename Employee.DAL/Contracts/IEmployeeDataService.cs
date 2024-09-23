using Employee.Models;

namespace Employee.DAL.Contracts
{
    public interface IEmployeeDataService
    {
        Task UpsertEmployeeAsync(EmployeeUpdateModel employeeModel);
        Task<List<EmployeeEntity>> GetAllEmployeesAsync();
        Task<List<EmployeeEntity>> GetAllEmployeesWithDependenciesAsync();
    }
}
