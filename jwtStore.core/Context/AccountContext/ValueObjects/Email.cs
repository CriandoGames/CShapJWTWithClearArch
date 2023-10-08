using System.Text.RegularExpressions;
using jwtStore.core.Context.AccountContext.ValueObjects;
using jwtStore.core.Context.SharedContext.Extensions;
using jwtStore.core.Context.SharedContext.ValueObjects;

namespace jwtStore.core.AccountContext.ValueObjects;


public partial class Email : ValueObject
{

    private const string Pattern = @"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$";

    public string Value { get; }
    public string Hash => Value.ToBase64();
    public Verification Verification { get; private set; } = new();


    public void ResendVerification()
        => Verification = new Verification();


    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email é obrigatorio!", nameof(value));

        Value = value.Trim().ToLower();

        if (Value.Length > 254)
            throw new ArgumentException("Email é longo de mais", nameof(value));

        if (Value.Length < 3)
            throw new ArgumentException("Email muito curto", nameof(value));

        if (!EmailRegex().IsMatch(Value))
            throw new ArgumentException("Email é invalido", nameof(value));
    }

    public static implicit operator string(Email email)
                                    => email?.ToString() ?? "";

    public static implicit operator Email(string email)
                                    => new(email);

    public override string ToString()
                            => Value;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

}