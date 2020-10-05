using System;

namespace Sudoku.Application.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string error)
            : base($"Неверный ход. {error}")
        {
            
        }
    }
}