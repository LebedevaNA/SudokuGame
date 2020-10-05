using MediatR;
using Sudoku.Application.Accounts.Models;

namespace Sudoku.Application.Accounts.Commands.LoginAccount
{
    public class LoginAccountCommand : IRequest<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}