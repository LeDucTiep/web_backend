using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Model;
using MISA.WebFresher2023.Demo.DL.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.UnitTests.Service
{
    [TestFixture]
    public class EmployeeServiceTest
    {
        private readonly static EmployeeOutPage _employeeOutPage = new()
        {
            EmployeeCode = "NV-0012",
            FullName = "Lê Đức Tiệp",
            Email = "tiep@gmail.com",
            PositionName = "Trưởng phòng",
            DepartmentName = "Phòng hành chính",
        };
        private readonly static EmployeePage _employeePage = new()
        {
            TotalRecord = 3,
            Data =
            new List<EmployeeOutPage>(){
                _employeeOutPage,
                _employeeOutPage,
                _employeeOutPage
            }
        };
        private IEmployeeRepository employeeRepository;

        private IMapper mapper;

        private EmployeeService employeeService;
        private void InitGetPage()
        {
            employeeRepository = Substitute.For<IEmployeeRepository>();

            mapper = Substitute.For<IMapper>();

            employeeService = new(employeeRepository, mapper);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetPageAsync_InvalidPageSize_ReturnsException(int pageSize)
        {
            // Arrange
            InitGetPage();

            // Act
            var exception = Assert.ThrowsAsync<PagingArgumentException>(async () => await employeeService.GetPageAsync(pageSize, 1, ""));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(PagingErrorMessage.InvalidPageSize));

            _ = employeeRepository.Received(0).GetPageAsync(pageSize, 1, "");
        }

        [Test]
        [TestCase(1)]
        [TestCase(999999999)]
        public async Task GetPageAsync_ValidPageSize_ReturnsEmployeePage(int pageSize)
        {
            // Arrange
            InitGetPage();

            employeeRepository.GetPageAsync(pageSize, 1, "").Returns(_employeePage);
            // Act
            EmployeePage employeePage = await employeeService.GetPageAsync(pageSize, 1, "");

            // Assert
            // Trả về đúng định dạng
            Assert.That(_employeePage, Is.SameAs(employeePage));

            // Chỉ gọi 1 lần 
            _ = await employeeRepository.Received(1).GetPageAsync(pageSize, 1, "");
        }
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetPageAsync_InvalidPageNumber_ReturnsException(int pageNumber)
        {
            // Arrange
            InitGetPage();

            // Act
            var exception = Assert.ThrowsAsync<PagingArgumentException>(async () => await employeeService.GetPageAsync(1, pageNumber, ""));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(PagingErrorMessage.InvalidPageNumber));

            _ = employeeRepository.Received(0).GetPageAsync(1, pageNumber, "");
        }

        [Test]
        [TestCase(1)]
        [TestCase(999999999)]
        public async Task GetPageAsync_ValidPageNumber_ReturnsEmployeePage(int pageNumber)
        {
            // Arrange
            InitGetPage();

            employeeRepository.GetPageAsync(1, pageNumber, "").Returns(_employeePage);
            // Act
            EmployeePage employeePage = await employeeService.GetPageAsync(1, pageNumber, "");

            // Assert
            // Trả về đúng định dạng
            Assert.That(_employeePage, Is.SameAs(employeePage));

            // Chỉ gọi 1 lần 
            _ = await employeeRepository.Received(1).GetPageAsync(1, pageNumber, "");
        }

        [Test]
        [TestCase("iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii")]
        public void GetPageAsync_InvalidEmployeeSearchTerm_ReturnsException(string employeeSearchTerm)
        {
            // Arrange
            InitGetPage();

            // Act
            var exception = Assert.ThrowsAsync<PagingArgumentException>(async () => await employeeService.GetPageAsync(1, 1, employeeSearchTerm));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(PagingErrorMessage.InvalidEmployeeSearchTerm));

            _ = employeeRepository.Received(0).GetPageAsync(1, 1, employeeSearchTerm);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ahihi")]
        [TestCase("iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii")]
        public async Task GetPageAsync_ValidEmployeeSearchTerm_ReturnsEmployeePage(string employeeSearchTerm)
        {
            // Arrange
            InitGetPage();

            employeeRepository.GetPageAsync(1, 1, employeeSearchTerm).Returns(_employeePage);
            // Act
            EmployeePage employeePage = await employeeService.GetPageAsync(1, 1, employeeSearchTerm);

            // Assert
            // Trả về đúng định dạng
            Assert.That(_employeePage, Is.SameAs(employeePage));

            // Chỉ gọi 1 lần 
            _ = await employeeRepository.Received(1).GetPageAsync(1, 1, employeeSearchTerm);
        }

        [Test]
        [TestCase("11452b0c-768e-5ff7-0d63-eeb1d8ed8cef")]
        public void DeleteAsync_IdNotFound_ReturnsException(Guid id)
        {
            // Arrange
            InitGetPage();
            employeeRepository.DeleteAsync(id).Returns(1001);

            // Act
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.DeleteAsync(id));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(EmployeeErrorMessage.IdNotFound));

            employeeRepository.Received(1).DeleteAsync(id);
        }

        [Test]
        [TestCase("11452b0c-768e-5ff7-0d63-eeb1d8ed8ced")]
        public void DeleteAsync_IdValid_ReturnsSuccess(Guid id)
        {
            // Arrange
            InitGetPage();
            employeeRepository.DeleteAsync(id).Returns(0);

            // Act && Assert
            Assert.DoesNotThrowAsync(async () => await employeeService.DeleteAsync(id));

            employeeRepository.Received(1).DeleteAsync(id);
        }
    }
}
