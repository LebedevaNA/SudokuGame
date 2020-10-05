using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Application.Accounts.Commands.LoginAccount;
using Sudoku.Domain;

namespace Sudoku.Auth.Api.Controllers
{
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginAccountCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
                return Unauthorized();
            return Ok(result);

            
        }

    }
}