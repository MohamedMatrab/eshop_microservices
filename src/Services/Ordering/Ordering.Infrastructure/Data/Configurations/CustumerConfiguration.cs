namespace Ordering.Infrastructure.Data.Configurations;

public class CustumerConfiguration : IEntityTypeConfiguration<Custumer>
{
    public void Configure(EntityTypeBuilder<Custumer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id).HasConversion(cId=>cId.Value,dbId=>CustumerId.Of(dbId));
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}