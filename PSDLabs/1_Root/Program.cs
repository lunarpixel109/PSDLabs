using System;
using GameCode;

class Program {
    static void Main(string[] args) {
        StartGame:
        MazeGame game = new MazeGame();
        game.Run();
        
        Console.WriteLine("Press escape to exit or press r to try again");
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
