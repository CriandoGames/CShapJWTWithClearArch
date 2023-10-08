using jwtStore.core.Context.SharedContext.Entities;


namespace jwtStore.core.Context.AccountContext.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
    }
}
