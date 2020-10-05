using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sudoku.Application.Exceptions;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Services;
using Sudoku.Domain;

namespace Sudoku.Application.GameSteps.Commands.CreateGameStep
{
    public class CreateGameStepCommandHandler : IRequestHandler<CreateGameStepCommand>
    {
        private readonly ISudokuDbContext _context;
        private readonly IGameService _gameService;

        public CreateGameStepCommandHandler(ISudokuDbContext context, IGameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        public async Task<Unit> Handle(CreateGameStepCommand request, CancellationToken cancellationToken)
        {
            //это можно перенести в пайплайн-валидатор. Я предаочитаю делать их два: один для запроса, а второй для сущностей 
            var account =
                await _context.Accounts
                    .FirstOrDefaultAsync(e => e.Email == request.AccountEmail, cancellationToken);
            if (account == null)
                throw new NotFoundException(nameof(Account), request.AccountEmail);

            var currentGame = await _context.Games
                .FirstOrDefaultAsync(e => e.Id == request.GameId, cancellationToken);
            if (currentGame == null)
                throw new NotFoundException(nameof(Game), request.GameId.ToString());

            if (currentGame.IsCompleted)
                throw new ConcurentException();

            var gameState = (int?[][]) JsonConvert.DeserializeObject(currentGame.CurrentState, typeof(int?[][]));

            if (gameState[request.X][request.Y] != null)
                throw new AlreadyExistsException($"GameState", $"[{request.X}][{request.Y}]");

            //вот до сюда в валидатор
            
            if (!_gameService.ValidateGameStep(gameState, request.X, request.Y, request.Value))
                throw new GameException("Не сходится!");
            
            //gameState здесь в виду int[][], но лучше, конечно, обренуть в класс. Туда же и некоторую функциональность, например, проверить ход и т.д.
            gameState[request.X][request.Y] = request.Value;
            currentGame.CurrentState = JsonConvert.SerializeObject(gameState);
            
            _context.GameSteps.Add(new GameStep
            {
                AccountId = account.Id,
                GameId = currentGame.Id,
                GameState = currentGame.CurrentState,
                Created = DateTime.Now
            });

            if (_gameService.IsGameSolved())
            {
                currentGame.IsCompleted = true;
                account.Rating += 10;
            }
            else
            {
                account.Rating += 1;}
            
            _context.SaveChangesAsync(cancellationToken);
            
            //тут если один из игроков оказался "вторым", то просто ничего не произойдет. В бд не обновится, ошибки клиент не получит тоже
            //как альтернатива, можно выкидывать возвращать клиенту ошибку, если измененных записей после savechanges не было
            return Unit.Value;

        }
    }
}