using AutoMapper;

namespace Sudoku.Application.Interfaces
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}