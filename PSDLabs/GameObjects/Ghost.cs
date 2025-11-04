using GameCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using ConsoleRenderingHelper;

namespace GameObjects
{
    class Ghost: GameObject {

        private int deltaX;
        private int deltaY;


        public Ghost(int startX, int startY, Colour colour) : base(startX, startY, colour, 'G') { }

        public override void Update(char[,] maze, ConsoleKey inputKey)
        {

            previousPositionX = positionX;
            previousPositionY = positionY;

            deltaX = -1;
            deltaY = 0;
            
            
            
            
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


    }
}
