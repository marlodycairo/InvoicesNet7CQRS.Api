using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoicesNet7CQRS.Api.Mediator
{
    public static class MediatorConfiguration
    {
        private static readonly List<string> AssemblyList = new()
        {
            "InvoicesNet7CQRS.Data",
            "InvoicesNet7CQRS.Services",
        };

        public static void AddMediaRConf(this IServiceCollection services)
        {
            foreach (var assembly in AssemblyList.Select(Assembly.Load))
            {
                services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(assembly));
            }
        }
    }
}
