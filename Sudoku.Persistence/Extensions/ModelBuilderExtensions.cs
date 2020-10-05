using Microsoft.EntityFrameworkCore;
using Sudoku.Domain;

namespace Sudoku.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1, Email = "gamer1@mail.ru",
                    Password = "1111",
                    Roles = new[] {Role.User}
                },
                new Account
                {
                    Id = 2, Email = "gamer2@mail.ru",
                    Password = "1111",
                    Roles = new[] {Role.User}
                });
            
        }
    }
}