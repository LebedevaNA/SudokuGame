using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Api.Models;
using Sudoku.Application.GameSteps.Commands.CreateGameStep;

namespace Sudoku.Api.Controllers
{
    [Route("api/GameSteps")]
    public class GameStepsController : Controller
    {
        private string Email => User.Claims.Single(e => e.Type == ClaimTypes.Email).Value;
        private readonly IMediator _mediator;

        public GameStepsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ApiResponse<object>> Index([FromBody] CreateGameStepCommand command)
        {
            command.AccountEmail = Email;
            
            await _mediator.Send(command);
            return new ApiResponse<object>();
        }
    }
}