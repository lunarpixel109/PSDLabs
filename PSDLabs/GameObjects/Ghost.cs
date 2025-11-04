using GameCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using ConsoleRenderingHelper;
using PSDLabs;

namespace GameObjects {
    class Ghost : GameObject
    {

        private int deltaX;
        private int deltaY;

        private Direction currentDirection;
        private Direction previousDirection;
        private Random random;

        public Ghost(int startX, int startY, Colour colour) : base(startX, startY, colour, '╳')
        {
            random = new Random();
        }

        public override void Update(char[,] maze, ConsoleKey inputKey)
        {

            previousPositionX = positionX;
            previousPositionY = positionY;

            deltaX = -1;
            deltaY = 0;

            currentDirection = new Direction(deltaX, deltaY);
            previousDirection = currentDirection;

            positionX += deltaX;
            positionY += deltaY;

            while (maze[positionX, positionY] == '▓')
            {
                positionX = previousPositionX;
                positionY = previousPositionY;

                Direction newDirection = new Direction(0, 0);

                do
                {
                    int direction = random.Next(0, 4);
                    switch (direction)
                    {
                        case 0:
                            newDirection = new Direction(0, -1); break;
                        case 1:
                            newDirection = new Direction(0, 1); break;
                        case 2:
                            newDirection = new Direction(1, 0); break;
                        case 3:
                            newDirection = new Direction(-1, 0); break;
                    }
                } while (newDirection == previousDirection.Invert());

                previousDirection = currentDirection;
                currentDirection = newDirection;

                deltaX = currentDirection.x;
                deltaY = currentDirection.y;

                positionX += deltaX;
                positionY += deltaY;
            }
        }
    }
}
