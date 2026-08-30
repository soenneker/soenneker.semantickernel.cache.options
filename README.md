[![](https://img.shields.io/nuget/v/soenneker.semantickernel.cache.options.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.cache.options/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.cache.options/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.cache.options/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.semantickernel.cache.options.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.cache.options/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.cache.options/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.cache.options/actions/workflows/codeql.yml)

# Soenneker.SemanticKernel.Cache.Options

A concurrent, keyed cache for lazily created `SemanticKernelOptions` instances.

## Installation

```bash
dotnet add package Soenneker.SemanticKernel.Cache.Options
```

## Registration

```csharp
using Soenneker.SemanticKernel.Cache.Options.Registrars;

services.AddSemanticKernelOptionsCacheAsSingleton();
```

Singleton registration shares option objects across consumers. `AddSemanticKernelOptionsCacheAsScoped()` creates an independent cache for each DI scope.

## Usage

```csharp
using Soenneker.SemanticKernel.Cache.Options.Abstract;
using Soenneker.SemanticKernel.Dtos.Options;

SemanticKernelOptions options = await optionsCache.Get(
    "primary-chat",
    () => ValueTask.FromResult(new SemanticKernelOptions
    {
        ModelId = configuration["Models:Primary:ModelId"],
        Endpoint = configuration["Models:Primary:Endpoint"],
        ApiKey = configuration["Models:Primary:ApiKey"],
        RequestsPerMinute = 60,
        MaxTokens = 2_000
    }),
    cancellationToken);
```

Concurrent callers for the same key share one initialization. The first successfully created object remains associated with that key; factories passed by later calls are not invoked until the entry is removed.

```csharp
bool removed = await optionsCache.Remove("primary-chat");
SemanticKernelOptions replacement = await optionsCache.Get(
    "primary-chat",
    () => LoadReplacementOptions());
```

## Important behavior

- Cached options are mutable and returned by reference. Treat them as read-only after publication, or coordinate mutations because every consumer of that key sees the same object.
- The `Get` cancellation token controls waiting for cache initialization, but the factory delegate itself does not receive that token. Capture a suitable token explicitly if the factory performs cancellable work.
- `GetAll` returns a dictionary snapshot; the contained option objects are the same cached references.
- `Clear` and disposal remove entries. They cannot erase immutable strings such as an API key already stored in an options object.

Use different keys for different model, endpoint, credential, rate-limit, or generation configurations. Removing a cached options entry does not remove a `Kernel` already created from it in a separate kernel cache.
