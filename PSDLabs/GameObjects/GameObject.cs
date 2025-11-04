using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameCode;
using ConsoleRenderingHelper;

namespace GameObjects
{

   
    
    abstract class GameObject
    {
        public int positionX, positionY, previousPositionX, previousPositionY;
        public Colour colour;
        public char sprite;

        public GameObject(int startX, int startY, Colour colour, char sprite)
        {
            positionX = startX;
            positionY = startY;
            previousPositionX = startX;
            previousPositionY = startY;
            this.colour = colour;
            this.sprite = sprite;
        }

        public abstract void Update(char[,] maze, ConsoleKey inputKey);

        public virtual void Draw(char[,] maze) {
            // Only update the position if it has changed
            if (positionX != previousPositionX || positionY != previousPositionY)
            {
                // Erase the previous position
                ConsoleRendering.WriteCharAtPoint(previousPositionX, previousPositionY, '░', MazeGame.foregroundColor, MazeGame.backgroundColor);
                maze[previousPositionY, previousPositionX] = '░';

                // Draw the player at the new position
                ConsoleRendering.WriteCharAtPoint(positionX, positionY, sprite, colour, MazeGame.backgroundColor);
                maze[positionY, positionX] = sprite;
            }
        }
    }
}
