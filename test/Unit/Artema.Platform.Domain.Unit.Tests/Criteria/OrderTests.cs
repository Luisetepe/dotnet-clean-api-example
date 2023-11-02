using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Exceptions;

namespace Artema.Platform.Domain.Unit.Tests.Criteria;

public class OrderTests
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Given_Bad_Inputs_OrderBy_Construction_Should_Fail(string? input)
    {
        Should.Throw<InvalidCriteriaException>(() => OrderBy.FromValue(input!))
            .Message.ShouldBe($"The input '{input}' was not a valid OrderBy field.");
    }
    
    [Theory]
    [InlineData("name")]
    [InlineData("phone-number")]
    [InlineData("date12")]
    [InlineData("p_v_p")]
    public void Given_Valid_Inputs_OrderBy_Construction_Should_Succeed(string input)
    {
        OrderBy.FromValue(input).Value.ShouldBe(input);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    [InlineData("hola")]
    [InlineData("descendent")]
    public void Given_Bad_Inputs_OrderType_Construction_Should_Fail(string? input)
    {
        Should.Throw<InvalidCriteriaException>(() => OrderType.FromValue(input!))
            .Message.ShouldBe($"The input '{input}' was not a valid OrderType operator.");
    }
    
    [Theory]
    [InlineData("asc", OrderTypeEnum.ASC)]
    [InlineData("desc", OrderTypeEnum.DESC)]
    public void Given_Valid_Inputs_OrderType_Construction_Should_Succeed(string input, OrderTypeEnum orderType)
    {
        OrderType.FromValue(input).Value.ShouldBe(orderType);
    }
    
}
