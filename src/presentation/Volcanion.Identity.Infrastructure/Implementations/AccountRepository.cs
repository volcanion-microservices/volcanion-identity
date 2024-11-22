using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Context;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

internal class AccountRepository : BaseRepository<Account, ApplicationDbContext>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context, ILogger<BaseRepository<Account, ApplicationDbContext>> logger) : base(context, logger)
    {
    }

    /// <inheritdoc/>
    public async Task<Account> GetAccountByEmail(string email)
    {
        try
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Email == email);
            return account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }
}
