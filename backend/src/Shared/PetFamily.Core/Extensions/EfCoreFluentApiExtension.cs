using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetFamily.Core.Extensions;

public static class EfCoreFluentApiExtension
{
    public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectCollectionJsonConversion<TValueObject>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder)
    {
        return builder.HasConversion(
                valueObjects => JsonSerializer.Serialize(valueObjects, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<TValueObject>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<TValueObject>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb");
    }
    
    public static PropertyBuilder<IReadOnlyList<TValueDtoObject>> ValueObjectDtoCollectionJsonConversion<TValueDtoObject>(
        this PropertyBuilder<IReadOnlyList<TValueDtoObject>> builder)
    {
        return builder.HasConversion(rq => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<TValueDtoObject>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<TValueDtoObject>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb");
    }
}