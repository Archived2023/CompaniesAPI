using Companies.EmployeeService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpServicesDemo.Tests
{
    public class CalculatorTests
    {
        private Calculator sut;

        public CalculatorTests()
        {
             sut = new Calculator();
        }

        [Theory]
        [InlineData(1,578)]
        [InlineData(6,2)]
        [InlineData(9,7)]
        public void Add(int val1, int val2)
        {
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);
        } 
        
        
        [Theory]
        [MemberData(nameof(GetNumbers), 2)]
        public void Add2(int val1, int val2)
        {
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);
        } 
        
        [Theory]
        [MemberData(nameof(GetNumbers2), 2)]
        public void Add3(int val1, int val2)
        {
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);
        }

        public static IEnumerable<object[]> GetNumbers(int nrOfDataSets)
        {
            var dataSets =  new List<object[]>
            {
                new object[] { 1, 2, },
                new object[] { 1, 2, },
                new object[] { 1, 2, },
                new object[] { 1, 2, }
            };

            return dataSets.Count <= nrOfDataSets ? dataSets: dataSets.Take(nrOfDataSets);
        } 
        
        public static IEnumerable<object[]> GetNumbers2(int nrOfDataSets)
        {
            var dataSets =  new TheoryData<int, int>
            {
                { 1, 2 },
                { 1, 2 },
                { 1, 2 },
                { 1, 2 }
            };

            return dataSets.Count() <= nrOfDataSets ? dataSets: dataSets.Take(nrOfDataSets);
        }
    }

    public class UseMemberDataFromAnotherClass
    {
        [Theory]
        [MemberData(nameof(CalculatorTests.GetNumbers), 2, MemberType = typeof(CalculatorTests))]
        public void Demo(int val1, int val2)
        {
            var sut = new Calculator();
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);

        }
    }
    
    public class UseMemberDataFromAnotherClass2
    {
        [Theory]
        [ClassData(typeof(ClassDataEample))]
        public void Demo(int val1, int val2)
        {
            var sut = new Calculator();
            var res = sut.Add(val1, val2);
            Assert.Equal(val1 + val2, res);

        }
    }

    public class ClassDataEample : TheoryData<int, int>
    {
        public ClassDataEample()
        {
            Add(1, 45);
            Add(1, 45);
            Add(1, 45);
            Add(1, 45);
        }
    }
}
