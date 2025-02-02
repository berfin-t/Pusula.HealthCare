using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class CountryConfigurations : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Countries", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).HasColumnName(nameof(Country.Name)).IsRequired()
         .HasMaxLength(CountryConsts.NameMaxLength);
        b.Property(x => x.Iso).HasColumnName(nameof(Country.Iso)).IsRequired()
         .HasMaxLength(CountryConsts.IsoMaxLength);
        b.Property(x => x.PhoneCode).HasColumnName(nameof(Country.PhoneCode)).IsRequired()
         .HasMaxLength(CountryConsts.PhoneCodeMaxLength);
    }
}