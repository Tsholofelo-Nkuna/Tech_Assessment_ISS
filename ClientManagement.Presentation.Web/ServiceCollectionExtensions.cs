using ClientManagement.Presentation.Web.Components.Pages.Clients.State.Details;
using ClientManagement.Presentation.Web.Components.Pages.Clients.State.Index;

namespace ClientManagement.Presentation.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStateManagers(this IServiceCollection services) {
            services.AddSingleton<IndexStateManager>();
            services.AddSingleton<DetailsStateManager>();
            return services;
        }
    }
}
