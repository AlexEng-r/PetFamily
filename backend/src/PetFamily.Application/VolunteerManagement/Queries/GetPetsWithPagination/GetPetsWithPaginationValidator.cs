using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetPetsWithPagination;

public class GetPetsWithPaginationValidator
    : AbstractValidator<GetPetsWithPaginationQuery>
{
    public GetPetsWithPaginationValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("Page"));

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}