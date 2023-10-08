using jwtStore.core.AccountContext.ValueObjects;
using jwtStore.core.Context.AccountContext.ValueObjects;
using jwtStore.core.Context.SharedContext.Entities;

namespace jwtStore.core.Context.AccountContext.Entities;

public class User : Entity
{

    protected User()
    {
    }

    public User(string email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }

    public User(string name, string email, Password password)
    {
        Email = email;
        Password =  password;
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public string Image { get; private set; } = string.Empty;

    public List<Role> Roles { get; set; } = new();


    public void UpdatePassword(string plainTextPassword, string resetCode)
    {
        if (!string.Equals(resetCode.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("Resete code invalido");

        Password = new Password(plainTextPassword);
    }


}