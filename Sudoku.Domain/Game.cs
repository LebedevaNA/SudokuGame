using System.Collections;
using System.Collections.Generic;

namespace Sudoku.Domain
{
    public class Game
    {
        public Game()
        {
            GameSteps = new List<GameStep>();
        }
        public long Id { get; set; }
        public string CurrentState { get; set; }
        public bool IsCompleted { get; set; }
        public byte[] Version { get; set; }

        public ICollection<GameStep> GameSteps { get; set; }
    }
}