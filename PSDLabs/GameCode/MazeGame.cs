using System;
using GameObjects;
using HighScoreLibrary;
using ConsoleRenderingHelper;


namespace GameCode {

    class MazeGame {
       // private Player player;
       public static char[,] Maze = new char[,] {
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'},//1
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//2
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//3
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', ' ', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//4
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//5
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//6
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//7
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//8
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//9
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', ' ', ' ', ' ', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//10
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//11
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//12
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//13
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//14
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//15
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//16
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//17
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//18
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//19
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//20
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//21
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'} //22
            };

        public static char[,] StartingMaze = new char[,] {
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'},//1
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//2
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//3
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', ' ', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//4
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//5
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//6
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//7
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//8
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//9
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', ' ', ' ', ' ', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//10
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//11
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//12
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//13
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//14
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//15
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//16
                {'▓', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', '▓'},//17
                {'▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓'},//18
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//19
                {'▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', ' ', '▓', '▓', '▓', '▓', '▓', ' ', '▓', '▓', '▓', '▓', ' ', '▓'},//20
                {'▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓'},//21
                {'▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓', '▓'} //22
            };
        private bool isRunning;
        public HighScoreManager highScoreManager;

        Player player;
        List<GameObject> gameObjects;

        public static Colour foregroundColor = new Colour(0, 143, 5);
        public static Colour backgroundColor = new Colour(66, 194, 100);

        private bool playerFirstMove = false;

        public bool isInPowerPellet = false;

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


                // Render only if the player has moved
                player.Draw(Maze);
                for (int i = 0; i < gameObjects.Count(); i++) {
                    gameObjects[i].Draw(Maze);
                }

                
                // Update
                player.Update(Maze, inputKey, deltaTime);
                if (playerFirstMove) {
                    for (int i = 0; i < gameObjects.Count(); i++) {
                        var gameObject = gameObjects[i];
                        gameObject.Update(Maze, inputKey, deltaTime);
                        player.CheckCollision(gameObject);
                    }
                }


                // Exit the game loop on ESC key
                if (inputKey == ConsoleKey.Escape) {
                    isRunning = false;
                    highScoreManager.EndOfGame(player.GetScore());
                }

                System.Threading.Thread.Sleep(100); // Control the game speed
            }
            
            
            highScoreManager.SortScores();
            highScoreManager.DisplayHighScores();
        }

        private void Initialize() {
            Maze = (char[,])StartingMaze.Clone();
            Console.Clear();
            highScoreManager = new HighScoreManager("highscores.json");
            gameObjects = new List<GameObject>();
            player = new Player(13, 20, this);
            gameObjects.Add(new Ghost(13, 10, new Colour(0, 255, 255), .5f, 0f, GhostType.INKY));
            gameObjects.Add(new Ghost(12, 10, new Colour(255, 0, 0), .5f, 1f, GhostType.BLINKY));
            gameObjects.Add(new Ghost(14, 10, new Colour(255, 184, 255), .5f, 2.5f, GhostType.PINKY));
            gameObjects.Add(new Ghost(11, 10, new Colour(255, 184, 82), .5f, 5f, GhostType.CLYDE));
            
            for (int y = 0; y < Maze.GetLength(0); y++) {
                for (int x = 0; x < Maze.GetLength(1); x++) {
                    if (Maze[y, x] == ' ') {
                        // Place power pellets at specific locations
                        if ((x == 1 && y == 1) || (x == 25 && y == 1) || (x == 1 && y == 20) || (x == 25 && y == 20)) {
                            gameObjects.Add(new Pellet(x, y, true));
                        } else {
                            gameObjects.Add(new Pellet(x, y, false));
                        }
                    }
                }
            }

            gameObjects.Add(new HUD(22, this));

            DrawMaze();

            player.OnCollision += HandleCollision;
            player.OnPositionUpdate += HandlePlayerUpdate;
            player.OnChangePowerPellet += HandlePowerPelletChange;
        }

        private void DrawMaze()
        {
            ConsoleRendering.DrawCharGrid(0, 0, StartingMaze, foregroundColor, backgroundColor);
        }

        private void HandlePowerPelletChange(bool active) {
            foreach (var gameObject in gameObjects) {
                if (gameObject is Ghost) {
                    Ghost ghost = (Ghost)gameObject;
                    ghost.SetRunningState(active);
                } else if (gameObject is HUD && active) {
                    HUD hud = (HUD)gameObject;
                    hud.StartCountdown();
                }
            }
            isInPowerPellet = active;
            if (!active) {
                Console.Beep(659, 100);
                Console.Beep(523, 100);
                Console.Beep(440, 100);
            }
        }

        private void HandleCollision() {
            if (!isInPowerPellet) {
                isRunning = false;
                highScoreManager.EndOfGame(player.GetScore());
            }
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

        public void DeleteObject(GameObject obj) {
            gameObjects.Remove(obj);
        }


        public int GetScore() {
            return player.GetScore();
        }
    }
}
