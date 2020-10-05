using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Sudoku.Application.Services
{
    public interface IGameService
    {
        int?[][] CreateNewGame();
        Task<bool> HasActiveGame();
        bool ValidateGameStep(int? [][] gameState, int x, int y, int value);
        public bool IsGameSolved();
    }
}