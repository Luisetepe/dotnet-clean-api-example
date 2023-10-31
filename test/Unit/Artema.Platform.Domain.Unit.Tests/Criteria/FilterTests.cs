using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Exceptions;
using Shouldly;

namespace Artema.Platform.Domain.Unit.Tests.Criteria;

public class FilterTests
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Given_Bad_Inputs_FilterField_Construction_Should_Fail(string? input)
    {
        Should.Throw<InvalidCriteriaException>(() => FilterField.FromValue(input!))
            .Message.ShouldBe($"The input '{input}' was not a valid Filter field.");
    }
    
    [Theory]
    [InlineData("name")]
    [InlineData("phone-number")]
    [InlineData("date12")]
    [InlineData("p_v_p")]
    public void Given_Valid_Inputs_FilterField_Construction_Should_Succeed(string input)
    {
        FilterField.FromValue(input).Value.ShouldBe(input);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    [InlineData("hola")]
    [InlineData("eqs")]
    [InlineData("cons")]
    public void Given_Bad_Inputs_FilterOperator_Construction_Should_Fail(string? input)
    {
        Should.Throw<InvalidCriteriaException>(() => FilterOperator.FromValue(input!))
            .Message.ShouldBe($"The input '{input}' was not a valid Filter operator.");
    }
    
    [Theory]
    [InlineData("eq", FilterOperatorEnum.EQ)]
    [InlineData("neq", FilterOperatorEnum.NEQ)]
    [InlineData("lt", FilterOperatorEnum.LT)]
    [InlineData("lte", FilterOperatorEnum.LTE)]
    [InlineData("gt", FilterOperatorEnum.GT)]
    [InlineData("gte", FilterOperatorEnum.GTE)]
    [InlineData("inc", FilterOperatorEnum.INC)]
    public void Given_Valid_Inputs_FilterOperator_Construction_Should_Succeed(string input, FilterOperatorEnum filterOperator)
    {
        FilterOperator.FromValue(input).Value.ShouldBe(filterOperator);
    }
}