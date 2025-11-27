using System;
using GameObjects;
using HighScoreLibrary;
using ConsoleRenderingHelper;


namespace GameCode {

    class MazeGame {
       // private Player player;
       public static char[,] Maze = new char[,] {
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
        private bool isRunning;
        private HighScoreManager highScoreManager;

        Player player;
        List<GameObject> gameObjects;

        public static Colour foregroundColor = new Colour(0, 143, 5);
        public static Colour backgroundColor = new Colour(0, 0, 0);

        private bool playerFirstMove = false;

        ConsoleKey inputKey;

        public MazeGame() {
            Initialize();
        }


        public void Run() {
            playerFirstMove = false;
            isRunning = true;
            DateTime dtPrev = DateTime.Now;
            DateTime dtNow = DateTime.Now;
            while (isRunning) {
                
                dtNow = DateTime.Now;
                float deltaTime = (float)dtNow.Subtract(dtPrev).TotalSeconds;
                dtPrev = dtNow;
                
                if (Console.KeyAvailable) {
                    inputKey = Console.ReadKey(true).Key;
                } else {
                    inputKey = ConsoleKey.None;
                }

                // Update
                player.Update(Maze, inputKey, deltaTime);
                if (playerFirstMove) {
                    foreach (var gameObject in gameObjects) {
                        gameObject.Update(Maze, inputKey, deltaTime);
                        player.CheckCollision(gameObject);
                    }
                }

                // Render only if the player has moved
                player.Draw(Maze);
                foreach (var gameObject in gameObjects) {
                    gameObject.Draw(Maze);
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
        }

        private void Initialize() {
            Console.Clear();
            highScoreManager = new HighScoreManager("highscores.json");
            gameObjects = new List<GameObject>();
            player = new Player(1, 1);
            gameObjects.Add(new Ghost(13, 10, new Colour(0, 255, 255), .05f, 0f, GhostType.INKY));
            gameObjects.Add(new Ghost(12, 10, new Colour(255, 0, 0), .5f, 1f, GhostType.BLINKY));
            //gameObjects.Add(new Ghost(14, 10, new Colour(255, 184, 255), .5f, 2.5f, GhostType.PINKY));
            gameObjects.Add(new Ghost(11, 10, new Colour(255, 184, 82), .5f, 5f, GhostType.CLYDE));
            
            DrawMaze();

            player.OnCollision += HandleCollision;
            player.OnPositionUpdate += HandlePlayerUpdate;
        }

        private void DrawMaze()
        {
            ConsoleRendering.DrawCharGrid(0, 0, Maze, foregroundColor, backgroundColor);
        }

        private void HandleCollision() {
            isRunning = false;
            highScoreManager.EndOfGame(player.getScore());
        }

        private void HandlePlayerUpdate(int x, int y) {
            foreach (var gameObject in gameObjects) {
                if (gameObject.GetType() == typeof(Ghost)) {
                    Ghost  ghost = (Ghost)gameObject;
                    ghost.UpdatePlayerPosition(x, y);
                }
            }
            playerFirstMove = true;
        }

    }
}
