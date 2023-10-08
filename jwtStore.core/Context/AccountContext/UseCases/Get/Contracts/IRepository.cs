using jwtStore.core.Context.AccountContext.Entities;


namespace jwtStore.core.Context.AccountContext.UseCases.Get.Contracts
{
    public interface IRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
