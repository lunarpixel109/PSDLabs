using ConsoleRenderingHelper;
using GameCode;

namespace GameObjects {
    class Player : GameObject {
        private int score;

        public Player(int startX, int startY): base(startX, startY, new Colour(143, 43 ,0), '╬') { }

        public delegate void CollisionEventHandler();
        public event CollisionEventHandler OnCollision;

        public delegate void PositionUpdate(int x, int y);
        public event PositionUpdate OnPositionUpdate;
        

        public override void Update(char[,] maze, ConsoleKey inputKey)
        {

            previousPositionX = positionX;
            previousPositionY = positionY;

            switch (inputKey)
            {
                case ConsoleKey.UpArrow:
                    Move(0, -1, maze);
                    score++;
                    break;
                case ConsoleKey.DownArrow:
                    Move(0, 1, maze);
                    score++;
                    break;
                case ConsoleKey.LeftArrow:
                    Move(-1, 0, maze);
                    score++;
                    break;
                case ConsoleKey.RightArrow:
                    Move(1, 0, maze);
                    score++;
                    break;
            }
            
        }
        
        private void Move(int deltaX, int deltaY, char[,] maze) {
            int newPosX = positionX + deltaX;
            int newPosY = positionY + deltaY;

            if (maze[newPosY, newPosX] == '░')
            {
                positionX = newPosX;
                positionY = newPosY;
                OnPositionUpdate?.Invoke(newPosX, newPosY);
            }
        }

        public void CheckCollision(GameObject other) {
            if (positionX == other.positionX && positionY == other.positionY) {
                OnCollision?.Invoke();
            }
        }

        public int getScore() {
            return score;
        }
    }
}