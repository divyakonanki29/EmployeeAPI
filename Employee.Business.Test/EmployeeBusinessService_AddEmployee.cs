using Employee.Business.Contracts;
using Employee.Business.Implementations;
using Employee.DAL.Contracts;
using Employee.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Employee.Business.Test
{
    public class EmployeeBusinessService_AddEmployee : IClassFixture<ServiceCollectionFixtures>
    {
        ServiceCollectionFixtures mockserviceCollectionFixtures;
        public EmployeeBusinessService_AddEmployee(ServiceCollectionFixtures serviceCollectionFixtures)
        {
            mockserviceCollectionFixtures = serviceCollectionFixtures;
        }
        [Fact]
        public async Task WhenNoEmployeeRequest_is_empty()
        {
            //initiate
            var serviceCollection = mockserviceCollectionFixtures.GetServiceDescriptors();
            serviceCollection.AddSingleton<IEmployeeBusinessService, EmployeeBusinessService>();

            //setup
            var employeeMockRepo = new Mock<IEmployeeDataService>();
            employeeMockRepo.Setup(x => x.UpsertEmployeeAsync(It.IsAny<EmployeeUpdateModel>()));
            serviceCollection.AddSingleton<IEmployeeDataService>(employeeMockRepo.Object);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var employeeBusinessService = serviceProvider.GetRequiredService<IEmployeeBusinessService>();

            //act
            await employeeBusinessService.AddOrUpdateEmployee(new EmployeeRequest { });

            //assert IF THE UPDATE METHOD IS CALLED AT LEAST ONCE
            employeeMockRepo.Verify(s => s.UpsertEmployeeAsync(It.IsAny<EmployeeUpdateModel>()), Times.Once);
        }

    }
}
