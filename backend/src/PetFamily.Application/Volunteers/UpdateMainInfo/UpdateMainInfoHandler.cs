using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Repositories;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.FullNames;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.String;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ICommonRepository _commonRepository;
    private readonly ILogger<UpdateMainInfoRequest> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoRequest> logger,
        ICommonRepository commonRepository)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _commonRepository = commonRepository;
    }

    public async Task<Result<Guid, Error>> Handle(UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }

        var dto = request.Dto;

        var fullName = FullName.Create(dto.FullName.FirstName, dto.FullName.Surname, dto.FullName.Patronymic).Value;
        var description = NotEmptyString.Create(dto.Description).Value;
        var phone = ContactPhone.Create(dto.Phone).Value;

        volunteer.Value.UpdateMainInfo(fullName, description, dto.Experience, phone);

        await _commonRepository.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", request.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}