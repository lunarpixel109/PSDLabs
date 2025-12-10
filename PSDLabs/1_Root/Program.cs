using System;
using GameCode;

class Program {
    static void Main(string[] args) {
        StartGame:
        MazeGame game = new MazeGame();
        game.Run();
        
        Console.WriteLine("Press R to retry or press any other key to try again");
        var key = Console.ReadKey(true);
        switch (key.Key) {
            case ConsoleKey.Escape:
                return;
            case ConsoleKey.R:
                game = null;
                goto StartGame;
        }
        
        
    }
}
