using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Core.Abstractions;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand
{
    Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}