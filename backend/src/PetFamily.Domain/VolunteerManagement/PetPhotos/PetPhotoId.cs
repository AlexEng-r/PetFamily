﻿namespace PetFamily.Domain.VolunteerManagement.PetPhotos;

public class PetPhotoId
{
    public Guid Value { get; }
    
    private PetPhotoId(Guid value)
    {
        Value = value;
    }

    public static PetPhotoId NewPetId() => new(Guid.NewGuid());

    public static PetPhotoId Empty() => new(Guid.Empty);

    public static PetPhotoId Create(Guid id) => new(id);
}