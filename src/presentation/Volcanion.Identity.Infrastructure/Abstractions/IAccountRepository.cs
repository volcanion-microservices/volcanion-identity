using Volcanion.Core.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Abstractions;

public interface IAccountRepository : IGenericRepository<Account>
{
    /// <summary>
    /// GetAccountByEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Account?> GetAccountByEmail(string email);
}
