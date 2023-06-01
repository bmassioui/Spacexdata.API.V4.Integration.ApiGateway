using FluentValidation;

namespace ApiGateway.Application.Features.Launches.Upcoming.Queries.GetUpcomingLaunchById;

public class GetUpcomingLaunchByIdQueryValidator : AbstractValidator<GetUpcomingLaunchByIdQuery>
{
    public GetUpcomingLaunchByIdQueryValidator()
    {
        RuleFor(v => v.UpcomingLaunchId)
           .NotNull()
           .NotEmpty()
           .WithMessage("Upcoming launch identifier cannot be null or empty.");
    }
}
