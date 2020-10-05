using System.Threading.Tasks;
using Sudoku.Domain;

namespace Sudoku.Application.Services
{
    public interface IJwtService
    {
        string GenerateAccountJwt(Account account);
    }
}