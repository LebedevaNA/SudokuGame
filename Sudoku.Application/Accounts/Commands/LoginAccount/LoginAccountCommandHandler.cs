using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sudoku.Application.Accounts.Models;
using Sudoku.Application.Accounts.Queries.GetAccountByEmailPassword;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Services;

namespace Sudoku.Application.Accounts.Commands.LoginAccount
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, LoginResult>
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;

        public LoginAccountCommandHandler(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _jwtService = jwtService;
        }

        public async Task<LoginResult> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            //Не стала хэшировать пароли, для простоты. В продакшне я так не делаю)
            var user = await _mediator.Send(new GetAccountByEmailPasswordQuery {Email = request.Email, Password = request.Password}, cancellationToken);
            
            if (user != null)
            {
                var token = _jwtService.GenerateAccountJwt(user);
                return new LoginResult {Access_token = token};
            }

            return null;
        }
    }
}