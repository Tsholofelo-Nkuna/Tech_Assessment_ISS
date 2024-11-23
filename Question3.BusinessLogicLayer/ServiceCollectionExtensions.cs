using Microsoft.Extensions.DependencyInjection;
using Question3.BusinessLogicLayer.Interfaces;
using Question3.BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer
{
    public static class ServiceCollectionExtensions
    {
       public static IServiceCollection AddBusinessServices(this IServiceCollection services) {
           return services
                .AddAutoMapper(typeof(AutoMapperConfig))
                .AddScoped<IClientService, ClientService> ();
          
        }
    }
}
