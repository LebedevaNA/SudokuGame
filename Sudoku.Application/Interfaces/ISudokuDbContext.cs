using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sudoku.Domain;

namespace Sudoku.Application.Interfaces
{
    public interface ISudokuDbContext : IDisposable
    {
         DbSet<Account> Accounts { get; set; }
         DbSet<Game> Games { get; set; }
         DbSet<GameStep> GameSteps { get; set; }

         Task<int> SaveChangesAsync(CancellationToken cancellationToken);
         void Migrate();
    }
}