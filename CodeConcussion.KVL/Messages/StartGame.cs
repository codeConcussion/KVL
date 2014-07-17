using CodeConcussion.KVL.Entity;

namespace CodeConcussion.KVL.Messages
{
    public sealed class StartGame
    {
        public StartGame(Game game)
        {
            Game = game;
        }

        public Game Game { get; set; }
    }
}