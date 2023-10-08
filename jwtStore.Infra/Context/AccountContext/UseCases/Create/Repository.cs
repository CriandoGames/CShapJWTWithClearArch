using jwtStore.core.AccountContext.ValueObjects;
using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Create.Contracts;
using jwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace jwtStore.Infra.Context.AccountContext.UseCases.Create
{
    public class Repository : IRepository
    {

        private readonly AppDbContext _context;


        public Repository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UserExistsAsync(Email email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Email.Value == email.Value, cancellationToken);
        }
    }
}
