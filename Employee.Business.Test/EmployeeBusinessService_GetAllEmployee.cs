using Employee.Business.Contracts;
using Employee.Business.Implementations;
using Employee.DAL.Contracts;
using Employee.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Employee.Business.Test
{
    public class EmployeeBusinessService_GetAllEmployee: IClassFixture<ServiceCollectionFixtures>
    {
        ServiceCollectionFixtures mockserviceCollectionFixtures;
        public EmployeeBusinessService_GetAllEmployee(ServiceCollectionFixtures serviceCollectionFixtures) {
            mockserviceCollectionFixtures = serviceCollectionFixtures;
        }
        [Fact]
        public async Task WhenNoEmployeesCreated()
        {
            //initiate
            var serviceCollection = mockserviceCollectionFixtures.GetServiceDescriptors();
            serviceCollection.AddSingleton<IEmployeeBusinessService, EmployeeBusinessService>();

            //setup
            var employeeMockRepo = new Mock<IEmployeeDataService>();
            employeeMockRepo.Setup(x => x.GetAllEmployeesWithDependenciesAsync()).Returns(Task.FromResult(new List<EmployeeEntity>()));
            serviceCollection.AddSingleton<IEmployeeDataService>(employeeMockRepo.Object);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var employeeBusinessService = serviceProvider.GetRequiredService<IEmployeeBusinessService>();

            //act
            var results = await employeeBusinessService.GetAllEmployee();

            //assert
            Assert.Empty(results);
        }

        [Fact]
        public async Task WhenEmployeesPersonalInformationExists()
        {
            //initiate
            var serviceCollection = mockserviceCollectionFixtures.GetServiceDescriptors();
            serviceCollection.AddSingleton<IEmployeeBusinessService, EmployeeBusinessService>();

            //setup
            var employeeMockRepo = new Mock<IEmployeeDataService>();
            employeeMockRepo.Setup(x => x.GetAllEmployeesWithDependenciesAsync()).Returns(Task.FromResult(new List<EmployeeEntity>()
            {
                new EmployeeEntity
                {
                    FirstName = "Kiran"
                }
            }));
            serviceCollection.AddSingleton<IEmployeeDataService>(employeeMockRepo.Object);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var employeeBusinessService = serviceProvider.GetRequiredService<IEmployeeBusinessService>();

            //act
            var results = await employeeBusinessService.GetAllEmployee();

            //assert
            Assert.NotEmpty(results);
            Assert.Equal("Kiran", (results?.FirstOrDefault()?.FirstName));
        }
    }
}