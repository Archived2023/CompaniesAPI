namespace Companies.API.Exceptions
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid id) : base($"The company with id: [{id}] doesen't exist")
        {
            
        }
    }

    //public class EmployeeNotFoundException : NotFoundException
    //{

    //}

    public class NotFoundException : Exception
    {
        public string Title { get; }
        public NotFoundException(string message, string title = "Not Found") : base(message)
        {
            Title = title;
        }

    }
}
