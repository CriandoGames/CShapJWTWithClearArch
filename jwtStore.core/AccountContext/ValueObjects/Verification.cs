using jwtStore.core.SharedContext.ValueObjects;

namespace jwtStore.core.AccountContext.ValueObjects;


public class Verification : ValueObject
{


    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;



    public void Verify(string code)
    {

        if (IsActive)
            throw new InvalidOperationException("Verification already verified");

        if (VerifiedAt != null)
            throw new InvalidOperationException("Verification already verified");

        if (ExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Verification expired");

        if (Code != code)
            throw new InvalidOperationException("Verification code is invalid");

        //ignora case sensitive
        if (!string.Equals(Code.Trim(), code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("Verification code is invalid");

        VerifiedAt = DateTime.UtcNow;
        ExpiresAt = null;


    }


}