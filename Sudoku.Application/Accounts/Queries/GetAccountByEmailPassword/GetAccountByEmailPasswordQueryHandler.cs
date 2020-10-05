using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Domain;

namespace Sudoku.Application.Accounts.Queries.GetAccountByEmailPassword
{
    public class GetAccountByEmailPasswordQueryHandler : IRequestHandler<GetAccountByEmailPasswordQuery, Account>
    {
        private readonly ISudokuDbContext _context;

        public GetAccountByEmailPasswordQueryHandler(ISudokuDbContext context)
        {
            _context = context;
        }

        public async Task<Account> Handle(GetAccountByEmailPasswordQuery request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == request.Email && e.Password == request.Password, cancellationToken);

            return account;
        }
    }
}