using FluentValidation;

namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchById;

public class GetPastLaunchByIdQueryValidator : AbstractValidator<GetPastLaunchByIdQuery>{
    public GetPastLaunchByIdQueryValidator()
    {
        RuleFor(v => v.PastLaunchId)
           .NotNull()
           .NotEmpty()
           .WithMessage("Past launch identifier cannot be null or empty.");
    }
}
