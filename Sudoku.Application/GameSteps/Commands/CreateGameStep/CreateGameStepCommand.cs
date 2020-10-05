using MediatR;

namespace Sudoku.Application.GameSteps.Commands.CreateGameStep
{
    public class CreateGameStepCommand : IRequest
    {
        public int GameId { get; set; }
        public string AccountEmail { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
    }
}