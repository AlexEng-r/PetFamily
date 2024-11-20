using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.Positions;

public record Position
{
    public int Value { get; }

    private Position(int value)
    {
        Value = value;
    }

    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
        {
            return Errors.General.ValueIsInvalid("serial number");
        }

        return new Position(number);
    }

    public Result<Position, Error> Forward()
        => Create(Value + 1);

    public Result<Position, Error> Back()
        => Create(Value - 1);

    public static Position First() => new(1);
}