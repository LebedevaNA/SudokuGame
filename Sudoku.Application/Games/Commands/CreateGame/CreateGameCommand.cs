using MediatR;

namespace Sudoku.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<Domain.Game>
    {
        public string AccountEmail { get; set; }
    }
}