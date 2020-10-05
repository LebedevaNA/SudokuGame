using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Accounts.Models;
using Sudoku.Application.Interfaces;

namespace Sudoku.Application.Accounts.Queries.GetTopAccounts
{
    public class GetTopAccountsQueryHandler : IRequestHandler<GetTopAccountsQuery, List<AccountDto>>
    {
        private readonly ISudokuDbContext _context;
        private readonly IMapper _mapper;

        public GetTopAccountsQueryHandler(ISudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AccountDto>> Handle(GetTopAccountsQuery request, CancellationToken cancellationToken)
        {
            //если нужно всех аккаутов, то скорее всего с пэджинацией
            var result = await _context.Accounts
                .AsNoTracking()
                .OrderByDescending(e => e.Rating)
                .Take(100)
                .ToListAsync(cancellationToken);
            
            return _mapper.Map<List<AccountDto>>(result);
        }
    }
}