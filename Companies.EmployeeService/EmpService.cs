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
            if (employee.SalaryLevel == SalaryLevel.Default) throw new ArgumentException();

            var salaryLevel = validator.ValidateSalaryLevel(employee);
            //Do alot of stuff...
            validator.ValidateName2(employee.Name);

            var res1 = validator.ValidateName(employee.Name);
            var res2 = validator.ValidateName(employee.Name);

            if (res1 && !res2) return true;
            else return false;
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
                res = true;
            }

            var res1 = validator.TestProp;
            var res2 = validator.TestProp;
            var res3 = validator.TestProp;

            validator.TestProp = "Banan";

            validator.MustBeInvoked();
            //validator.ValidateName2("Kalle");
            return res; ;
        }
    }

    public class Employee
    {
        public string Name { get; set; } = string.Empty;
        public int Salary { get; set; }
        public SalaryLevel SalaryLevel { get; set; }
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
        bool ValidateName(string name);
        void ValidateName2(string name);
        void MustBeInvoked();
        string TestProp { get; set; }
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
