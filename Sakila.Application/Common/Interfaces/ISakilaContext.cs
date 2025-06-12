using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Models;

namespace Sakila.Application.Common.Interfaces;

public interface ISakilaContext
{
    DbSet<Country> Countries { get; }
    DbSet<Language> Languages { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
