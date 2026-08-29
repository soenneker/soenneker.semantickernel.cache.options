[![](https://img.shields.io/nuget/v/soenneker.semantickernel.cache.options.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.cache.options/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.cache.options/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.cache.options/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.semantickernel.cache.options.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.cache.options/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.cache.options/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.cache.options/actions/workflows/codeql.yml)

# Soenneker.SemanticKernel.Cache.Options

A cache for `SemanticKernelOptions` using a SingletonDictionary with support for keyed asynchronous creation.

## Install

```bash
dotnet add package Soenneker.SemanticKernel.Cache.Options
```

## Quick start

```csharp
using Soenneker.SemanticKernel.Cache.Options.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddSemanticKernelOptionsCacheAsSingleton();
```

Adds `ISemanticKernelOptionsCache` as a singleton service.

## What you get

- `ISemanticKernelOptionsCache` — A cache for `SemanticKernelOptions` using a SingletonDictionary with support for keyed asynchronous creation.
- `SemanticKernelOptionsCacheRegistrar` — Providing async thread-safe singleton Semantic Kernel Options instances.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ISemanticKernelOptionsCache.Get(key, optionsFactory, cancellationToken)` | Gets an existing `SemanticKernelOptions` from the cache, or creates and caches one using the provided factory. | The cached or newly created `SemanticKernelOptions`. |
| `ISemanticKernelOptionsCache.Remove(key)` | Removes an entry from the cache. | A task representing the asynchronous remove operation. |
| `ISemanticKernelOptionsCache.GetAll(cancellationToken)` | Retrieves all cached `SemanticKernelOptions` entries, keyed by their cache keys. | A dictionary of all keys and their corresponding `SemanticKernelOptions` values. |
| `ISemanticKernelOptionsCache.Clear(cancellationToken)` | Clears all entries from the cache. | A task representing the asynchronous clear operation. |
| `SemanticKernelOptionsCacheRegistrar.AddSemanticKernelOptionsCacheAsSingleton(services)` | Adds `ISemanticKernelOptionsCache` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `SemanticKernelOptionsCacheRegistrar.AddSemanticKernelOptionsCacheAsScoped(services)` | Adds `ISemanticKernelOptionsCache` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
