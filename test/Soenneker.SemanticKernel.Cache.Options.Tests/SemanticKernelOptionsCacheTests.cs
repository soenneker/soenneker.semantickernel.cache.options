using Soenneker.SemanticKernel.Cache.Options.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.SemanticKernel.Cache.Options.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class SemanticKernelOptionsCacheTests : HostedUnitTest
{
    private readonly ISemanticKernelOptionsCache _util;

    public SemanticKernelOptionsCacheTests(Host host) : base(host)
    {
        _util = Resolve<ISemanticKernelOptionsCache>(true);
    }

    [Test]
    public void Default()
    {

    }
}
