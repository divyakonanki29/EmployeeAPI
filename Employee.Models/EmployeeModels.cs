using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Employee.Models
{

    #region Basic properties for table
    public class BaseEntity: IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public interface IBaseEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public enum EmployeeTypes
    {
        Contract,
        Supervisor,
        Manager
    }
    #endregion

    interface IBaseEmployee : IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeTypes EmployeeType { get; set; }

    }

    public class AddressEntity : BaseEntity, IAddress
    {
        public string EmployeeId { get; set; }
        public string AddressLine { get; set; }

        [JsonIgnore]
        public EmployeeEntity employeeEntity { get; set; }
    }

    public interface IAddress
    {
        public string AddressLine { get; set; }
    }
    
    public class EmployeeEntity : IBaseEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeTypes EmployeeType { get; set; }
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public EmployeeSalaryComponentEntity EmployeeSalaryComponent { get; set; }
        public AddressEntity Address { get; set; }
    }
    public class ContractEmployee : EmployeeEntity, IAddress, IEmpPayPerHr
    {
        public decimal PayPerHour { get; set; }
        public string AddressLine { get; set; }
    }

    public class ManagerEmployee : EmployeeEntity, IAddress, IEmpAnnual, IEmpAllowances
    {
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }
        public string AddressLine { get; set; }
    }

    public class SupervisorEmployee : EmployeeEntity, IAddress, IEmpAnnual
    {
        public decimal AnnualSalary { get; set; }
        public string AddressLine { get; set; }
    }

    public class EmployeeRequest
    {
        public string? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeTypes EmployeeType { get; set; }
        public string AddressLine { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }
        public decimal PayPerHour { get; set; }
    }
    public interface ISalaryComponents : IEmpAnnual, IEmpAllowances, IEmpPayPerHr
    {
    }
    public class EmployeeSalaryComponentEntity : BaseEntity, ISalaryComponents
    {
        public string EmployeeId { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }
        public decimal PayPerHour { get; set; }

        [JsonIgnore]
        public EmployeeEntity employeeEntity { get; set; }
    }

    public interface IEmpAnnual 
    {
        public decimal AnnualSalary { get; set; }
    }

    public interface IEmpAllowances
    {
        public decimal MaxExpenseAmount { get; set; }
    }

    public interface IEmpPayPerHr
    {
        public decimal PayPerHour { get; set; }
    }

    public class EmployeeUpdateModel
    {
        public string EmployeeId { get; set; }
        public EmployeeSalaryComponentEntity employeeSalaryComponentEntity { get; set; }
        public EmployeeEntity employeeEntity { get; set; }
        public AddressEntity employeeAddressEntity { get; set; }

    }

}
