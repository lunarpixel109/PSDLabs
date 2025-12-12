using ConsoleRenderingHelper;
using GameCode;

namespace GameObjects {
    class Player : GameObject {
        

        public Player(int startX, int startY, MazeGame game): base(startX, startY, new Colour(252, 186, 3), '▚') { this.game = game; }

        public delegate void CollisionEventHandler();
        public event CollisionEventHandler OnCollision;

        public delegate void PositionUpdate(int x, int y);
        public event PositionUpdate OnPositionUpdate;

        public delegate void ChangePowerPellet(bool active);
        public event ChangePowerPellet OnChangePowerPellet;

        private int score;

        private bool powerPelletActive = false;
        private float powerPelletDuration = 10.0f;
        private float powerPelletTimer = 0.0f;

        private MazeGame game;

        public override void Update(char[,] maze, ConsoleKey inputKey, float deltaTime)
        {

            previousPositionX = positionX;
            previousPositionY = positionY;

            switch (inputKey)
            {
                case ConsoleKey.UpArrow:
                    Move(0, -1, maze);
                    break;
                case ConsoleKey.DownArrow:
                    Move(0, 1, maze);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(-1, 0, maze);
                    break;
                case ConsoleKey.RightArrow:
                    Move(1, 0, maze);
                    break;
            }

            if (powerPelletActive && powerPelletTimer > 0) {
                powerPelletTimer -= deltaTime;
            } else {
               if (powerPelletActive) {
                    powerPelletActive = false;
                    OnChangePowerPellet?.Invoke(false);
                }
                powerPelletTimer = 0.0f;
            }
            
        }
        
        private void Move(int deltaX, int deltaY, char[,] maze) {
            int newPosX = positionX + deltaX;
            int newPosY = positionY + deltaY;

            if (maze[newPosY, newPosX] != '▓')
            {
                positionX = newPosX;
                positionY = newPosY;
                OnPositionUpdate?.Invoke(newPosX, newPosY);
            }
        }

        public void CheckCollision(GameObject other) {
            if (positionX == other.positionX && positionY == other.positionY) {
                if (other is Pellet) {
                    if (((Pellet)other).isPowerPellet) {
                        Console.Beep(440, 100);
                        Console.Beep(523, 100);
                        Console.Beep(659, 100);
                        score += 100;
                        powerPelletActive = true;
                        powerPelletTimer = powerPelletDuration;
                        OnChangePowerPellet?.Invoke(true);
                    } else {
                        score += 50;
                        Console.Beep(440, 100);
                    }
                    game.DeleteObject(other);
                } else if (!game.isInPowerPellet) {
                     OnCollision?.Invoke();
                } else if (game.isInPowerPellet && other is Ghost) {
                    Console.Beep(880, 100);
                    score += 500;
                    var ghost = (Ghost)other;
                    ghost.Reset();
                }
               
            }
        }

        public int GetScore() {
            return score;
        }
    }
}