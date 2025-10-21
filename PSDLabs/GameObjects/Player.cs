namespace GameObjects
{
    class Player
    {
        private int posX;
        private int posY;
        private int prevX;
        private int prevY;
        private int score;

        public Player(int startX, int startY)
        {
            posX = startX;
            posY = startY;
            prevX = startX;
            prevY = startY;
        }

        public void Update(char[,] maze, ConsoleKey key)
        {
            prevX = posX;
            prevY = posY;

            switch (key)
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
            
        }

        public void Draw()
        {
            // Only update the position if it has changed
            if (posX != prevX || posY != prevY)
            {
                // Erase the previous position
                Console.SetCursorPosition(prevX, prevY);
                Console.Write(' ');

                // Draw the player at the new position
                Console.SetCursorPosition(posX, posY);
                Console.Write('P');

                score++;
            }
        }

        private void Move(int deltaX, int deltaY, char[,] maze)
        {
            int newPosX = posX + deltaX;
            int newPosY = posY + deltaY;

            if (maze[newPosY, newPosX] == ' ')
            {
                posX = newPosX;
                posY = newPosY;
            }
        }

        public int getScore()
        { 
            return score;
        }
    }
}