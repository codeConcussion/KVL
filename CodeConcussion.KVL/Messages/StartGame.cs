﻿using CodeConcussion.KVL.Entities;

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