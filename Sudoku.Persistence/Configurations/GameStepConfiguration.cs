using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sudoku.Domain;

namespace Sudoku.Persistence.Configurations
{
    public class GameStepConfiguration : IEntityTypeConfiguration<GameStep>
    {
        public void Configure(EntityTypeBuilder<GameStep> builder)
        {
            builder.Property(e => e.Id).UseIdentityColumn();

            builder.HasOne(e => e.Game)
                .WithMany(e => e.GameSteps)
                .HasForeignKey(e => e.GameId);
        }
    }
}