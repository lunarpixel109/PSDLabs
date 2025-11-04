using System;
using GameObjects;
using HighScoreLibrary;
using ConsoleRenderingHelper;


namespace GameCode {

    class MazeGame {
       // private Player player;
        private char[,] maze;
        private bool isRunning;
        private HighScoreManager highScoreManager;

        Player player;
        List<GameObject> gameObjects;

        public static Colour foregroundColor = new Colour(0, 143, 5);
        public static Colour backgroundColor = new Colour(0, 0, 0);


        ConsoleKey inputKey;

        public MazeGame() {
            Initialize();
            highScoreManager = new HighScoreManager("highscores.txt");
        }


        public void Run() {
            isRunning = true;

            while (isRunning) {

                if (Console.KeyAvailable) {
                    inputKey = Console.ReadKey(true).Key;
                } else {
                    inputKey = ConsoleKey.None;
                }

                // Update
                player.Update(maze, inputKey);
                foreach (var gameObject in gameObjects) {
                    gameObject.Update(maze, inputKey);
                    player.CheckCollision(gameObject);
                }

                // Render only if the player has moved
                player.Draw(maze);
                foreach (var gameObject in gameObjects) {
                    gameObject.Draw(maze);
                }




                // Exit the game loop on ESC key
                if (inputKey == ConsoleKey.Escape) {
                    isRunning = false;
                    highScoreManager.EndOfGame(player.getScore());
                }

                System.Threading.Thread.Sleep(100); // Control the game speed
            }

            highScoreManager.SortScores();
            highScoreManager.DisplayHighScores();
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }

        private void Initialize() {
            gameObjects = new List<GameObject>();
            player = new Player(1, 1);
            gameObjects.Add(new Ghost(13, 10, new Colour(0, 0, 255)));

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

            player.OnCollision += HandleCollision;
        }

        private void DrawMaze()
        {
            ConsoleRendering.DrawCharGrid(0, 0, maze, foregroundColor, backgroundColor);
        }

        private void HandleCollision() {
            isRunning = false;
            highScoreManager.EndOfGame(player.getScore());
        }

    }
}
