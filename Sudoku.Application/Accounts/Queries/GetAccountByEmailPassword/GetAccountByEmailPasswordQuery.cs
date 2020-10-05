using MediatR;
using Sudoku.Domain;

namespace Sudoku.Application.Accounts.Queries.GetAccountByEmailPassword
{
    public class GetAccountByEmailPasswordQuery : IRequest<Account>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}