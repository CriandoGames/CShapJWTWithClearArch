using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.core.Context.AccountContext.UseCases.Get.Contracts;
using jwtStore.Infra.Data;
using System.Data.Entity;

namespace jwtStore.Infra.Context.AccountContext.UseCases.Get
{
    public class Repository : IRepository
    {

        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;
        
        public async Task<User?> GetByEmailAsync(string email)
        {
           return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Value == email);
        }
    }
}
