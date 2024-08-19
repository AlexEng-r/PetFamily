﻿using PetFamily.Domain.Contacts;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Repositories.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default);
}