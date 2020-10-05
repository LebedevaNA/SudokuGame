using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sudoku.Domain;

namespace Sudoku.Persistence.Configurations
{
    public class AccountsConfuguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(e => e.Id)
                .UseIdentityColumn();
                
            builder.Property(e => e.Roles)
                .HasConversion(
                    v => String.Join(", ", v.ToList().Select(e => e.ToString())),
                    v => v.Split(", ", StringSplitOptions.None).Select(e => (Role) Enum.Parse(typeof(Role), e))
                        .ToArray()
                );
            
            
        }
    }
}