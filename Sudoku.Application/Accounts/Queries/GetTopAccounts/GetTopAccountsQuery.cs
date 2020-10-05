using System.Collections.Generic;
using MediatR;
using Sudoku.Application.Accounts.Models;

namespace Sudoku.Application.Accounts.Queries.GetTopAccounts
{
    public class GetTopAccountsQuery : IRequest<List<AccountDto>>
    {
        
    }
}