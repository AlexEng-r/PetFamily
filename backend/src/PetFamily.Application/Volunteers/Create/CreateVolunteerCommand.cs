﻿using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<RequisiteDto>? Requisites,
    IReadOnlyList<SocialNetworksDto>? SocialNetworks);