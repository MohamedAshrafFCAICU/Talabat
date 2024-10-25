using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance._Common;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
