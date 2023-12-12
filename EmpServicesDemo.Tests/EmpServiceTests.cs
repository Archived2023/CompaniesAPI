using Companies.EmployeeService;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace EmpServicesDemo.Tests
{
    public class EmpServiceTests
    {
        private const string expectedText = "Text";
        private Mock<IValidator> mockValidator;

        public EmpServiceTests()
        {
            mockValidator = new Mock<IValidator>();
        }

        [Fact]
        public void RegisterUser_WhenIncorrectName_ShouldReturnFalse()
        {
            const string incorrectEmployeeName = "K This is a incorrect name for an employee";


            var employee = new Employee
            {
                Name = incorrectEmployeeName,
                SalaryLevel = SalaryLevel.NotSet

            };

            mockValidator.Setup(x => x.ValidateName(employee)).Returns(false);
            mockValidator.Setup(x => x.ValidateName(It.IsAny<string>())).Returns(false);
            mockValidator.Setup(x => x.ValidateSalaryLevel(employee)).Returns(SalaryLevel.Default);
            mockValidator.Setup(x => x.ValidateName2(It.Is<string>(x => x.StartsWith('K'))));

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.RegisterUser(employee);

            Assert.False(actual);
        } 
        
        [Fact]
        public void RegisterUser2()
        {


            var employee = new Employee
            {
                Name = "Pelle",
                SalaryLevel = SalaryLevel.NotSet
            };

            mockValidator.SetupSequence(x => x.ValidateName(It.IsAny<string>())).Returns(true).Returns(false);

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.RegisterUser(employee);

            Assert.True(actual);
        }

        [Fact]
        public void HandleMessage_ShouldReturnTrueIfMatch()
        {

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

        [Fact]
        public void HandleMessage_VerifyMustBeInvoked_ShouldRun_Once()
        {
            mockValidator.Setup(x => x.Handler.CheckMessage.Message).Returns(expectedText);

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.HandleMessage(expectedText);

            mockValidator.Verify(x => x.MustBeInvoked(), Times.Exactly(2));
        } 
        
        [Fact]
        public void HandleMessage_WhenMessageMatch_TestPropGetAndSet()
        {
            mockValidator.Setup(x => x.Handler.CheckMessage.Message).Returns(expectedText);

            var sut = new EmpService(mockValidator.Object);
            var actual = sut.HandleMessage(expectedText);

            mockValidator.Verify(x => x.TestProp);
            mockValidator.VerifyGet(x => x.TestProp);

            mockValidator.VerifySet(x => x.TestProp = It.IsAny<string>());
            mockValidator.VerifySet(x => x.TestProp = It.Is<string>(s => s.Length < 10));
            mockValidator.VerifySet(x => x.TestProp = "Banan");

            mockValidator.VerifyGet(x => x.Handler.CheckMessage.Message);
            mockValidator.Verify(x => x.MustBeInvoked(), Times.Exactly(2));
            mockValidator.VerifyNoOtherCalls();
        }

        [Fact]
        public void RegisterUser_WhenSalaryLevelIsDefault_SholuldThrowArgumentException()
        {
            var sut = new EmpService(Mock.Of<IValidator>());
            Assert.Throws<ArgumentException>(() => sut.RegisterUser(new Employee()));
        }
    }
}