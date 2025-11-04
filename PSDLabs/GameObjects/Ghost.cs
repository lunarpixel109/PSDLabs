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

    enum GhostType {
        INKY,
        BLINKY,
        PINKY,
        CLYDE
    }
    
    class Ghost : GameObject
    {

        private int deltaX;
        private int deltaY;
        private int targetX;
        private int targetY;
        
        private Random random;

        private GhostType type;

        
        
        private int currentPlayerX;
        private int currentPlayerY;

        public void UpdatePlayerPosition(int x, int y) {
            currentPlayerX = x;
            currentPlayerY = y;
        }
        
        
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

            switch (type) {
                case GhostType.INKY:
                    MoveInky();
                    break;
                case GhostType.BLINKY:
                    MoveBlinky();
                    break;
                case GhostType.PINKY:
                    MovePinky();
                    break;
                case GhostType.CLYDE:
                    MoveClyde();
                    break;
            }
            
        }

        private void MoveInky() {
            // Targets the players position directly
            targetX = currentPlayerX;
            targetY = currentPlayerY;
            
            int directionX = positionX - currentPlayerX;
            int directionY = positionY - currentPlayerY;
            
            

        }

        private void MoveBlinky() {
            
        }

        private void MovePinky() {
            
        }

        private void MoveClyde() {
            
        }
    }
}
