﻿namespace Solimus.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
