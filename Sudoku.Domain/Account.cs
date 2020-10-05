using System;
using System.Runtime;

namespace Sudoku.Domain
{
    public class Account
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
        public int Rating { get; set; }
    }
    public enum Role
    {
        User,
        Admin
    }
}