﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Core.Abstractions;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery
{
    //Task<TResponse> Handle(TQuery command, CancellationToken cancellationToken);

    Task<Result<TResponse, ErrorList>> Handle(TQuery query, CancellationToken cancellationToken);
}

public interface IQueryHandler<TResponse>
{
    Task<Result<TResponse, ErrorList>> Handle(CancellationToken cancellationToken);
}