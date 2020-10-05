using System;

namespace Sudoku.Domain
{
    public class GameStep
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long AccountId { get; set; }
        public string GameState { get; set; }
        public DateTime Created { get; set; }

        public Game Game { get; set; }
    }
}