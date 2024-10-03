using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Abstractions;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery
{
    //Task<TResponse> Handle(TQuery command, CancellationToken cancellationToken);
    
    Task<Result<TResponse, ErrorList>> Handle(TQuery command, CancellationToken cancellationToken);
}