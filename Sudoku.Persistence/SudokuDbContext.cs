using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Domain;
using Sudoku.Persistence.Extensions;

namespace Sudoku.Persistence
{
    public class SudokuDbContext : DbContext, ISudokuDbContext
    {
        public SudokuDbContext(DbContextOptions<SudokuDbContext> options)
        : base(options)
        { }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameStep> GameSteps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SudokuDbContext).Assembly);
            modelBuilder.Seed();
        }
        
        public void Migrate()
        {
            Database.Migrate();
        }
    }
}