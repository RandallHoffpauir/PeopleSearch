using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property<string>(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property<string>(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
