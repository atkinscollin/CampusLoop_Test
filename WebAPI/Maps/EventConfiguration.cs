using System.Data.Entity.ModelConfiguration;
using UCEvents_WebAPI.Models;

namespace UCEvents_WebAPI.Maps
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration() : this("dbo")
        {
        }

        public EventConfiguration(string schema)
        {
            ToTable("Event", schema);

            HasKey(x => x.Id);

            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("varchar").HasMaxLength(254).IsRequired();
            Property(x => x.Location).HasColumnName(@"Location").HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(x => x.Date).HasColumnName(@"Date").HasColumnType("datetime").IsRequired();
            Property(x => x.PhotoURL).HasColumnName(@"PhotoURL").HasColumnType("varchar(max)").IsRequired();
            Property(x => x.Tag1).HasColumnName(@"Tag1").HasColumnType("varchar(max)").IsRequired();
        }
    }
}