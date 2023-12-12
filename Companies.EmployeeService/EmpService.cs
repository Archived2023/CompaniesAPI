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
            var salaryLevel = validator.ValidateSalaryLevel(employee);
            //Do alot of stuff...
            return validator.ValidateName(employee);
        }

        public bool HandleMessage(string text)
        {
            if(validator.Handler.CheckMessage.Message != text)
            {
                return false;
            }

            return true;
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
        IHandler Handler { get; }
        SalaryLevel ValidateSalaryLevel(Employee employee);
        bool ValidateName(Employee employee);
    }

    public interface IHandler
    {
        IMessage CheckMessage { get;  }
    }

    public interface IMessage
    {
        public string Message { get; }
    }
}
