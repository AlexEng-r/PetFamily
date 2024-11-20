using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Queries.GetVolunteersWithPagination;

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