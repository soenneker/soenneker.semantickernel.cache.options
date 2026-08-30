using Soenneker.SemanticKernel.Dtos.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.SemanticKernel.Cache.Options.Abstract;

/// <summary>
/// Provides concurrent, keyed creation and reuse of <see cref="SemanticKernelOptions"/> instances.
/// </summary>
public interface ISemanticKernelOptionsCache : IAsyncDisposable
{
    /// <summary>
    /// Gets an existing <see cref="SemanticKernelOptions"/> from the cache, or creates and caches one using the provided factory.
    /// </summary>
    /// <param name="key">The unique cache key.</param>
    /// <param name="optionsFactory">A factory function that returns a <see cref="SemanticKernelOptions"/> instance asynchronously.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The cached or newly created <see cref="SemanticKernelOptions"/>.</returns>
    ValueTask<SemanticKernelOptions> Get(string key, Func<ValueTask<SemanticKernelOptions>> optionsFactory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an entry from the cache.
    /// </summary>
    /// <param name="key">The unique key of the entry to remove.</param>
    /// <returns>A task representing the asynchronous remove operation.</returns>
    ValueTask<bool> Remove(string key);

    /// <summary>
    /// Retrieves all cached <see cref="SemanticKernelOptions"/> entries, keyed by their cache keys.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A dictionary snapshot containing each key and its cached <see cref="SemanticKernelOptions"/> reference.</returns>
    ValueTask<Dictionary<string, SemanticKernelOptions>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears all entries from the cache.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous clear operation.</returns>
    ValueTask Clear(CancellationToken cancellationToken = default);
}
