using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Api.Models;
using Sudoku.Application.Accounts.Queries.GetTopAccounts;

namespace Sudoku.Api.Controllers
{
    [Route("api/Accounts")]
    public class Accounts : Controller
    {
        private readonly IMediator _mediator;

        public Accounts(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ApiResponse<object>> GetTop()
        {
            var result = await _mediator.Send(new GetTopAccountsQuery());
            return new ApiResponse<object>(result);
        }
    }
}