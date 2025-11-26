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


        private List<Node> currentPath;
        private Pathfinding pathfinder;
        private int currentPathIndex;
        
        private float timeBetweenMovements;
        private float movementTimer;
        private bool canMove;
        
        private int currentPlayerX;
        private int currentPlayerY;

        public void UpdatePlayerPosition(int x, int y) {
            currentPlayerX = x;
            currentPlayerY = y;
        }
        
        
        public Ghost(int startX, int startY, Colour colour, float timeBetweenMoves, GhostType ghostType) : base(startX, startY, colour, '╳')
        {
            random = new Random();
            
            timeBetweenMovements = timeBetweenMoves;
            canMove = true;
            pathfinder = new Pathfinding();
            
            type = ghostType;
        }

        public override void Update(char[,] maze, ConsoleKey inputKey, float deltaTime)
        {

            

            if (canMove)
            {
                previousPositionX = positionX;
                previousPositionY = positionY;

                //if (currentPlayerX == positionX && currentPlayerY == positionY) // Only recalculate the path if the player has moved
                //{
                    switch (type)
                    {
                        case GhostType.INKY:
                            SetTargetInky();
                            break;
                        case GhostType.BLINKY:
                            SetTargetBlinky();
                            break;
                        case GhostType.PINKY:
                            SetTargetPinky();
                            break;
                        case GhostType.CLYDE:
                            SetTargetClyde();
                            break;
                    }
                //}

                var directionX = positionX - currentPath[currentPathIndex].x;
                var directionY = positionY - currentPath[currentPathIndex].y;

                positionX += deltaX;
                positionY += deltaY;

                movementTimer = 0;
                canMove = false;
            }

            if (movementTimer < timeBetweenMovements)
            {
                movementTimer += deltaTime;
                canMove = false;
            }
            else
            {
                canMove = true;
            }


        }

        private void SetTargetInky() {
            // Targets the players position directly
            targetX = currentPlayerX;
            targetY = currentPlayerY;
            
            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
        }
        
        private void SetTargetBlinky() {
            
            
        }

        private void SetTargetPinky() {
            
        }

        private void SetTargetClyde() {
            
        }
        
        
    }
}
