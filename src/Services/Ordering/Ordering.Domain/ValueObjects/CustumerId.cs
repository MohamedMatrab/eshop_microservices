namespace Ordering.Domain.ValueObjects;

public record CustumerId
{
    public Guid Value { get; }
    private CustumerId(Guid value) => Value = value;

    public static CustumerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("CustumerId cannot be empty");
        }
        return new CustumerId(value);
    }
}