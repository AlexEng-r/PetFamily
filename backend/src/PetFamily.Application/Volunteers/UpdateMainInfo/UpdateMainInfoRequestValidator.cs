﻿using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoRequestValidator
    : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateMainInfoRequestDtoValidator
    : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoRequestDtoValidator()
    {
        RuleFor(x => x.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.Surname, x.Patronymic));
        RuleFor(x => x.Description).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.Experience).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("Experience"));
        RuleFor(x => x.Phone).MustBeValueObject(ContactPhone.Create);
    }
}