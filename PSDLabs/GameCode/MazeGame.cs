using System;
using GameObjects;
using HighScoreLibrary;

namespace GameCode
{

    class MazeGame
    {
       // private Player player;
        private char[,] maze;
        private bool isRunning;
        private HighScoreManager highScoreManager;

        Player player;
        List<GameObject> gameObjects;

        public static ConsoleColor foregroundColor = ConsoleColor.DarkGreen;
        public static ConsoleColor backgroundColor = ConsoleColor.Black;


        ConsoleKey inputKey;

        public MazeGame()
        {
            Initialize();
            highScoreManager = new HighScoreManager("highscores.txt");
        }


        public void Run()
        {
            isRunning = true;

            while (isRunning)
            {

                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;

                if (Console.KeyAvailable)
                {
                    inputKey = Console.ReadKey(true).Key;
                } else
                {
                    inputKey = ConsoleKey.None;
                }

                // Update
                player.Update(maze, inputKey);
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Update(maze, inputKey);
                }

                // Render only if the player has moved
                player.Draw(maze);
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Draw(maze);
                }




                // Exit the game loop on ESC key
                if (inputKey == ConsoleKey.Escape)
                {
                    isRunning = false;
                    highScoreManager.EndOfGame(player.getScore());
                }

                System.Threading.Thread.Sleep(100); // Control the game speed
            }

            highScoreManager.SortScores();
            highScoreManager.DisplayHighScores();
        }

        private void Initialize()
        {
            gameObjects = new List<GameObject>();
            player = new Player(1, 1);
            gameObjects.Add(new Ghost(13, 10, ConsoleColor.Blue));

            maze = new char[,] {
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'},//1
                {'▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓'},//2
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//3
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '░', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//4
                {'▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓'},//5
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//6
                {'▓', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '▓'},//7
                {'▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓'},//8
                {'▓', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '▓'},//9
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '░', '░', '░', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//10
                {'▓', '░', '░', '░', '░', '░', '░', '▓', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '▓', '░', '░', '░', '░', '░', '░', '▓'},//11
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//12
                {'▓', '░', '░', '░', '░', '▓', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '▓', '░', '░', '░', '░', '▓'},//13
                {'▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓'},//14
                {'▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓'},//15
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//16
                {'▓', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '▓'},//17
                {'▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓'},//18
                {'▓', '░', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '▓', '░', '░', '░', '░', '░', '░', '▓'},//19
                {'▓', '░', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '░', '▓', '▓', '▓', '▓', '▓', '░', '▓', '▓', '▓', '▓', '░', '▓'},//20
                {'▓', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '░', '▓'},//21
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'} //22
            };
            DrawMaze();
        }

        private void DrawMaze()
        {

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(maze[y, x]);
                }
            }
        }

    }
}
