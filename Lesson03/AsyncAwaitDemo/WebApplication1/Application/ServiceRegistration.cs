using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    static public class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            //serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
