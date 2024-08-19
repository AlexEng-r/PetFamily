using PetFamily.Domain.SeedWork;

namespace PetFamily.API.Response;

public record Envelope
{
    public object? Result { get;  }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeCreated { get; }

    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeCreated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) => new(result, null);

    public static Envelope Error(Error error) => new(null, error);
}