using System;
using System.Dynamic;

namespace Sudoku.Application.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string entityType, string key)
            : base($"сущность {entityType} с ключом {key} уже существует")
        {
            
        }
    }
}