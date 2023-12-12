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
            validator.ValidateName2(employee.Name);

            return validator.ValidateName(employee);
        }

        public bool HandleMessage(string text)
        {
            bool res;
            if(validator.Handler.CheckMessage.Message != text)
            {
                res = false;
            }
            else
            {
                validator.MustBeInvoked();
               // validator.MustBeInvoked();
                res = true;
            }

            return res; ;
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
        void ValidateName2(string name);
        void MustBeInvoked();
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
