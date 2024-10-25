using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistance._Identity.Config
{
    [DbContextTypeAttribute(typeof(StoreIdentityDbContext))]
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FirstName)).HasColumnType("nVarchar").HasMaxLength(50);
            builder.Property(nameof(Address.LastName)).HasColumnType("nVarchar").HasMaxLength(50);
            builder.Property(nameof(Address.Street)).HasColumnType("Varchar").HasMaxLength(50);
            builder.Property(nameof(Address.City)).HasColumnType("Varchar").HasMaxLength(50);
            builder.Property(nameof(Address.Country)).HasColumnType("Varchar").HasMaxLength(50);


            builder.ToTable("Addresses");
        }
    }
}
