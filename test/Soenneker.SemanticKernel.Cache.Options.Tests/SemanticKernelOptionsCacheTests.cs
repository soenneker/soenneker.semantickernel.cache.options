using Soenneker.SemanticKernel.Cache.Options.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.SemanticKernel.Cache.Options.Tests;

[Collection("Collection")]
public class SemanticKernelOptionsCacheTests : FixturedUnitTest
{
    private readonly ISemanticKernelOptionsCache _util;

    public SemanticKernelOptionsCacheTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ISemanticKernelOptionsCache>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
