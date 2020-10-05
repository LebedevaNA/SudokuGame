using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sudoku.Persistence
{
    public class SudokuContextDesignTimeFactory : IDesignTimeDbContextFactory<SudokuDbContext>
    {
        public SudokuDbContext CreateDbContext(string[] args)
        {
            var optionnsBuilder = new DbContextOptionsBuilder<SudokuDbContext>();
            optionnsBuilder.UseSqlServer(
                "Data Source=cs-cyfral-test;Initial Catalog=testt;user id=sa;Password=Zab%BP@;");
            return new SudokuDbContext(optionnsBuilder.Options);
        }
    }
}