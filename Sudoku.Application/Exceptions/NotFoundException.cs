using System;

namespace Sudoku.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, string key)
            : base($"Сущность {entityName} с ключом {key} не найдена")
        { }
    }
}