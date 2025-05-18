using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.SemanticKernel.Cache.Options.Abstract;
using Soenneker.SemanticKernel.Dtos.Options;
using Soenneker.Utils.SingletonDictionary;

namespace Soenneker.SemanticKernel.Cache.Options;

///<inheritdoc cref="ISemanticKernelOptionsCache"/>
public sealed class SemanticKernelOptionsCache : ISemanticKernelOptionsCache
{
    private readonly SingletonDictionary<SemanticKernelOptions> _options;

    public SemanticKernelOptionsCache()
    {
        _options = new SingletonDictionary<SemanticKernelOptions>(CreateFromFactory);
    }

    private static ValueTask<SemanticKernelOptions> CreateFromFactory(string key, CancellationToken token, object[] args)
    {
        if (args.Length != 1 || args[0] is not Func<ValueTask<SemanticKernelOptions>> factory)
            throw new ArgumentException("Expected args: Func<ValueTask<SemanticKernelOptions>>");

        return factory();
    }

    public ValueTask<SemanticKernelOptions> Get(string key, Func<ValueTask<SemanticKernelOptions>> optionsFactory, CancellationToken cancellationToken = default)
    {
        return _options.Get(key, cancellationToken, optionsFactory);
    }

    public ValueTask Remove(string key)
    {
        return _options.Remove(key);
    }

    public ValueTask<Dictionary<string, SemanticKernelOptions>> GetAll(CancellationToken cancellationToken = default)
    {
        return _options.GetAll(cancellationToken);
    }

    public ValueTask Clear(CancellationToken cancellationToken = default)
    {
        return _options.Clear(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _options.DisposeAsync();
    }
}
