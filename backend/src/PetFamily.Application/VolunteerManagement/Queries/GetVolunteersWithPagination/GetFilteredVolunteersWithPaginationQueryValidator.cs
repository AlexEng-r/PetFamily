using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetFilteredVolunteersWithPaginationQueryValidator
    : AbstractValidator<GetFilteredVolunteersWithPaginationQuery>
{
    public GetFilteredVolunteersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Experience)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("Experience"));
        
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("Page"));
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}