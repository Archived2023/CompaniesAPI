namespace Companies.EmployeeService
{
    public class EmpService
    {
        private readonly IValidator validator;

        public EmpService(IValidator validator)
        {
            this.validator = validator;
        }

        public bool RegisterUser(Employee employee)
        {
            //Do alot of stuff...
            return validator.ValidateName(employee);
        }
    }

    public class Employee
    {
        public string Name { get; set; } = string.Empty;
        public int Salary { get; set; }
        public SalaryLevel salaryLevel { get; set; }
    }

    public enum SalaryLevel
    {
        Default,
        NotSet,
        Junior,
        Senior
    }

    public interface IValidator
    {
        SalaryLevel ValidateSalaryLevel(Employee employee);
        bool ValidateName(Employee employee);
    }
}
