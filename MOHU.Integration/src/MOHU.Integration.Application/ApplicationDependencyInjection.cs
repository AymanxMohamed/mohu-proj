using Microsoft.Extensions.DependencyInjection;
using MOHU.Integration.Application.common;
using MOHU.Integration.Application.Service;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.Application
{

    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IIvrService, IvrService>();
            services.AddTransient<IIndividualService, IndividualService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IHttpExceptionService, HttpExceptionService>();



            return services;
        }
    }
}
