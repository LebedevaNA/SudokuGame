using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;

namespace Sudoku.Application.Services
{
    public class GameService : IGameService
    {
        private readonly ISudokuDbContext _context;

        public GameService(ISudokuDbContext context)
        {
            _context = context;
        }

        public int?[][] CreateNewGame()
        {
            return new int?[][]
            {
                new int? [] {5, 3, null, null, 7, null, null, null, null},
                new int?[] {6, null, null, 1, 9, 5, null, null, null},
                new int?[] {null, 9, 8, null, null, null, null, 6, null},
                new int?[] {8, null, null, null, 6, null, null, null, 3},
                new int?[] {4, null, null, 8, null, 3, null, null, 1},
                new int?[] {7, null, null, null, 2, null, null, null, 6},
                new int?[] {null, 6, null, null, null, null, 2, 8, null},
                new int?[] {null, null, null, 4, 1, 9, null, null, 5},
                new int?[] {null, null,  null, null, 8, null, null, 7, 9}, 
            };
        }
        
        public async Task<bool> HasActiveGame()
        {
            var hasone = await _context.Games.AsNoTracking().AnyAsync();
            var hasoneactive =
                await _context.Games.AsNoTracking().AnyAsync(e => e.IsCompleted == false);

            if (!hasone)
                return false;

            return hasoneactive;
        }

        public bool ValidateGameStep(int?[][] gameState, int x, int y, int value)
        {
            //очень умный алгоритм проверки хода
            return true;
        }

        public bool IsGameSolved()
        {
            return false;
        }
    }
}