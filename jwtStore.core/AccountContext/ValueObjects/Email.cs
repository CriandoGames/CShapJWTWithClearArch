using System.Text.RegularExpressions;
using jwtStore.core.SharedContext.Extensions;
using jwtStore.core.SharedContext.ValueObjects;

namespace jwtStore.core.AccountContext.ValueObjects;


public partial class Email : ValueObject
{

    private const string Pattern = @"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$";

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required", nameof(value));

        Value = value.Trim().ToLower();

        if (Value.Length > 254)
            throw new ArgumentException("Email is too long", nameof(value));

        if (Value.Length < 5)
            throw new ArgumentException("Email is too short", nameof(value));

        if (!EmailRegex().IsMatch(Value))
            throw new ArgumentException("Email is invalid", nameof(value));
    }

    public static implicit operator string(Email email) => email?.ToString() ?? "";

    public static implicit operator Email(string email) => new(email);


    public override string ToString() => Value;



    public string Value { get; }
    public string Hash => Value.ToBase64();

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

}