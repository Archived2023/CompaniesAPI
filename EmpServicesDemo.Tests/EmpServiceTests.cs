using Companies.EmployeeService;
using Moq;

namespace EmpServicesDemo.Tests
{
    public class EmpServiceTests
    {
        [Fact]
        public void RegisterUser_WhenIncorrectName_ShouldReturnFalse()
        {
            const string incorrectEmployeeName = "This is a incorrect name for an employee";

            var mockValidator = new Mock<IValidator>(MockBehavior.Strict);

            var employee = new Employee
            {
                Name = incorrectEmployeeName
            };

            mockValidator.Setup(x => x.ValidateName(employee)).Returns(false);
            mockValidator.Setup(x => x.ValidateSalaryLevel(employee)).Returns(SalaryLevel.Default);

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.RegisterUser(employee);

            Assert.False(actual);
        }

        [Fact]
        public void HandleMessage_ShouldReturnTrueIfMatch()
        {
            const string expectedText = "Text";
            //var iMessageMock = new Mock<IMessage>();
            //iMessageMock.Setup(x => x.Message).Returns("Text");

            //var iHandlerMock = new Mock<IHandler>();
            //iHandlerMock.Setup(x => x.CheckMessage).Returns(iMessageMock.Object);

            //var mockValidator = new Mock<IValidator>();
            //mockValidator.Setup(x => x.Handler).Returns(iHandlerMock.Object);

            //var mockValidator = new Mock<IValidator>();
            //mockValidator.Setup(x => x.Handler.CheckMessage.Message).Returns(expectedText);

            var validator = Mock.Of<IValidator>(x => x.Handler.CheckMessage.Message == expectedText);

            var sut = new EmpService(validator);
            var actual = sut.HandleMessage(expectedText);

            Assert.True(actual);
        }
    }
}