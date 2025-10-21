using GameCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameObjects
{
    class Ghost: GameObject
    {


        public Ghost(int startX, int startY, ConsoleColor colour) : base(startX, startY, colour) { }

        public override void Update(char[,] maze, ConsoleKey inputKey)
        {

            previousPositionX = positionX;
            previousPositionY = positionY;

            switch (inputKey)
            {
                case ConsoleKey.W:
                    Move(0, -1, maze);
                    break;
                case ConsoleKey.S:
                    Move(0, 1, maze);
                    break;
                case ConsoleKey.A:
                    Move(-1, 0, maze);
                    break;
                case ConsoleKey.D:
                    Move(1, 0, maze);
                    break;
            }
        }

        private void Move(int deltaX, int deltaY, char[,] maze)
        {
            int newPosX = positionX + deltaX;
            int newPosY = positionY + deltaY;

            if (maze[newPosY, newPosX] == '░')
            {
                positionX = newPosX;
                positionY = newPosY;
            }
        }


        public override void Draw(char[,] maze)
        {
            // Only update the position if it has changed
            if (positionX != previousPositionX || positionY != previousPositionY)
            {
                // Erase the previous position
                Console.SetCursorPosition(previousPositionX, previousPositionY);
                Console.ForegroundColor = MazeGame.foregroundColor;
                Console.Write('░');
                

                // Draw the player at the new position
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = colour;
                Console.Write('E');
            }
        }


    }
}
