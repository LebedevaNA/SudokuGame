using AutoMapper;
using Sudoku.Application.Interfaces;
using Sudoku.Domain;

namespace Sudoku.Application.Accounts.Models
{
    public class AccountDto : IHaveCustomMapping
    {
        public string Email { get; set; }
        public string Rating { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Account, AccountDto>();
        }
    }
}