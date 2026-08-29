using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.SemanticKernel.Cache.Options.Abstract;

namespace Soenneker.SemanticKernel.Cache.Options.Registrars;

/// <summary>
/// Providing async thread-safe singleton Semantic Kernel Options instances
/// </summary>
public static class SemanticKernelOptionsCacheRegistrar
{
    /// <summary>
    /// Adds <see cref="ISemanticKernelOptionsCache"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddSemanticKernelOptionsCacheAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<ISemanticKernelOptionsCache, SemanticKernelOptionsCache>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ISemanticKernelOptionsCache"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddSemanticKernelOptionsCacheAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ISemanticKernelOptionsCache, SemanticKernelOptionsCache>();

        return services;
    }
}
