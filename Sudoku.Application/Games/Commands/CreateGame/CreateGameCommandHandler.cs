using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sudoku.Application.Exceptions;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Services;
using Sudoku.Domain;

namespace Sudoku.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Domain.Game>
    {
        private readonly ISudokuDbContext _context;
        private readonly IGameService _gameService;

        public CreateGameCommandHandler(ISudokuDbContext context, IGameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        public async Task<Domain.Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            //здесь для простоты считаю, что игра всегда одна. Пока она не завершилась, новую создать нельзя 
            if (await _gameService.HasActiveGame())
                throw new AlreadyExistsException(nameof(Game), "isCompleted=true");
            
            var account =
                _context.Accounts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Email == request.AccountEmail, cancellationToken);
            
            if (account == null)
                throw new NotFoundException(nameof(Account), request.AccountEmail);

            var gameState = _gameService.CreateNewGame();
            var gameStateJson = JsonConvert.SerializeObject(gameState);
            
            var game = _context.Games.Add(new Domain.Game
            {
                IsCompleted = false,
                CurrentState = gameStateJson, 
                GameSteps =
                {
                    new GameStep
                    {
                        Created = DateTime.Now,
                        AccountId = account.Id,
                        GameState = gameStateJson
                        
                    }
                }
            });

            await _context.SaveChangesAsync(cancellationToken);

            return game.Entity;
        }

    }
}