using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    abstract class GameObject
    {
        public int positionX, positionY, previousPositionX, previousPositionY;
        public ConsoleColor colour;

        public GameObject(int startX, int startY, ConsoleColor colour)
        {
            positionX = startX;
            positionY = startY;
            previousPositionX = startX;
            previousPositionY = startY;
            this.colour = colour;
        }

        public abstract void Update(char[,] maze, ConsoleKey inputKey);
        public abstract void Draw(char[,] maze);
    }
}
