using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Api.Models;
using Sudoku.Application.Exceptions;
using Sudoku.Application.Games.Commands.CreateGame;
using Sudoku.Domain;

namespace Sudoku.Api.Controllers
{
    [Route("api/Games")]
    public class GamesController : Controller
    {
        private string Email => User.Claims.Single(e => e.Type == ClaimTypes.Email).Value;
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ApiResponse<Game>> Get()
        {
            var result = await _mediator.Send(new CreateGameCommand {AccountEmail = Email});
            return new ApiResponse<Game>(result);
        }
        
    }
}