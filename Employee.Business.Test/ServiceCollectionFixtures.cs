using Employee.Business.Contracts;
using Employee.DAL.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Test
{

    public class ServiceCollectionFixtures
    {
       public IServiceCollection GetServiceDescriptors()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IEmployeeBusinessService>(new Mock<IEmployeeBusinessService>().Object);
            serviceCollection.AddSingleton<IEmployeeDataService>(new Mock<IEmployeeDataService>().Object);
            return serviceCollection;
        }
    }
}
