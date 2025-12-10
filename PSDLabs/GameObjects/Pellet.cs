using ConsoleRenderingHelper;
using GameCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects {
    class Pellet: GameObject {

        public bool isPowerPellet;


        public Pellet(int startX, int startY, bool isPowerPellet) : base(startX, startY, new Colour(255, 255, 255), MazeGame.backgroundColor, isPowerPellet ? '`' : '*') {
            this.isPowerPellet = isPowerPellet;
        }


        public override void Update(char[,] maze, ConsoleKey inputKey, float deltaTime) {}

        public override void Draw(char[,] maze) {
            // if the spot is blank and the pellet is still there, draw it
            if (maze[positionY, positionX] == ' ') {
                ConsoleRendering.WriteCharAtPoint(positionX, positionY, sprite, colour, MazeGame.backgroundColor);
            }
        }
    }
}
