using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Authenticate.Contracts;
using jwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace jwtStore.Infra.Context.AccountContext.UseCases.Authenticate
{
    public class Repository : IRepository
    {

        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;


        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
             await _context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email.Value == email, cancellationToken);

    }
}
