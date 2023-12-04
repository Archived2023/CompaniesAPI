namespace Companies.Client.Helpers
{
    public class UriHelpers
    {
        private const string root = "api/companies";
        private const string include = "?includeEmployees=true";
        public static string GetCompany(bool includeEmployees = false) => includeEmployees ? $"{root}/{include}" : $"{root}";
        public static string GetCompany(string companyId) => $"{root}/{companyId}";
        public static string GetEmployeesForCompany(string companyId) => $"{root}/{companyId}/employees";
        public static string GetEmployeeForCompany(string companyId, string employeeId) => $"{root}/{companyId}/employees/{employeeId}";
    }
}
