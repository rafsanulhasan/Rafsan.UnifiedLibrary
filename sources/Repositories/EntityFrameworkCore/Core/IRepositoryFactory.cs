using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Rafsan.DataAccess.Repositories;

namespace Rafsan.DataAccess.EntityFrameworkCore;

/// <summary>
/// Generates new instances to encapsulate the 'Repository' pattern
/// in scenarios where injected types may be long-lived (e.g. Blazor)
/// </summary>
public partial interface IRepositoryFactory
{
}
