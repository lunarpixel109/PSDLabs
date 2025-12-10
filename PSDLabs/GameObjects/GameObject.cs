using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameCode;
using ConsoleRenderingHelper;

namespace GameObjects {

   
    
    abstract class GameObject {
        public int positionX, positionY, previousPositionX, previousPositionY;
        public Colour colour;
        public Colour BackgroundColour;
        public char sprite;

        public GameObject(int startX, int startY, Colour colour, char sprite) {
            positionX = startX;
            positionY = startY;
            // set to an invalid position so the object is rendered at least once
            previousPositionX = -1;
            previousPositionY = -1;
            this.colour = colour;
            this.sprite = sprite;
        }

        public GameObject(int startX, int startY, Colour colourForeground, Colour colourBackground, char sprite) {
            positionX = startX;
            positionY = startY;
            // set to an invalid position so the object is rendered at least once
            previousPositionX = -1;
            previousPositionY = -1;
            colour = colourForeground;
            BackgroundColour = colourBackground;
            this.sprite = sprite;
        }

        public abstract void Update(char[,] maze, ConsoleKey inputKey, float deltaTime);

        public virtual void Draw(char[,] maze) {
            bool hasPrev = previousPositionX >= 0 && previousPositionY >= 0;

            // Draw when this is the first draw (no previous) or the position has changed
            if (!hasPrev || positionX != previousPositionX || positionY != previousPositionY) {
                // Erase the previous position only if there was a valid previous position
                if (hasPrev) {
                    ConsoleRendering.WriteCharAtPoint(previousPositionX, previousPositionY, ' ', MazeGame.foregroundColor, MazeGame.backgroundColor);
                }

                // Draw the object at the current position
                ConsoleRendering.WriteCharAtPoint(positionX, positionY, sprite, colour, MazeGame.backgroundColor);
            }
        }
    }
}
