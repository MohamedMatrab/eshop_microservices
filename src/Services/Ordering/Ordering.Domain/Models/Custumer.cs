namespace Ordering.Domain.Models;

public class Custumer : Entity<CustumerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Custumer Create(CustumerId Id,string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

        return new Custumer
        {
            Id =Id,
            Name = name,
            Email = email
        };
    }
}