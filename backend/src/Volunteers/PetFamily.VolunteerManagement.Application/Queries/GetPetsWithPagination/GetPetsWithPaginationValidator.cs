using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Queries.GetPetsWithPagination;

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