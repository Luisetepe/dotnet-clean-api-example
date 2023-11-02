using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Artema.Platform.Api.Endpoints.GetProductById;

public record GetProductByIdRequest
{
    [FromRoute]
    public Guid Id { get; init; }
}

public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}