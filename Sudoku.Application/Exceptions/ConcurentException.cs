using System;

namespace Sudoku.Application.Exceptions
{
    public class ConcurentException : Exception
    {
        public ConcurentException()
        :base ("Не успел")
        {
            
        }
    }
}