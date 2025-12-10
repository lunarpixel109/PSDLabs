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
    
    class Ghost : GameObject {

        private int deltaX;
        private int deltaY;
        private int targetX;
        private int targetY;
        
        private Random random;

        private GhostType type;

        private Colour calmColour;

        private List<Node> currentPath;
        private int currentPathIndex;

        private int startX;
        private int startY;
        
        private float timeBetweenMovements;
        private float movementTimer;
        private bool canMove;

        private float releaseTimer;
        private float releaseTime;
        
        private int currentPlayerX;
        private int currentPlayerY;
        private int previousPlayerX;
        private int previousPlayerY;

        private bool isRunning = false;

        public void UpdatePlayerPosition(int x, int y) {
            
            previousPlayerX = currentPlayerX;
            previousPlayerY = currentPlayerY;
            
            currentPlayerX = x;
            currentPlayerY = y;
        }
        
        public void SetRunningState(bool running) {
            isRunning = running;
        }

        public Ghost(int startX, int startY, Colour colour, float timeBetweenMoves, float releaseTime, GhostType ghostType) : base(startX, startY, colour, 'G') {
            random = new Random();
            
            timeBetweenMovements = timeBetweenMoves;
            canMove = true;
            type = ghostType;
            
            this.releaseTime = releaseTime;
            releaseTimer     = 0;

            currentPlayerX = 0;
            currentPlayerY = 0;

            calmColour = colour;

            this.startX = startX;
            this.startY = startY;
        }

        public override void Update(char[,] maze, ConsoleKey inputKey, float deltaTime) {

            previousPositionX = positionX;
            previousPositionY = positionY;
            
            if (isRunning) {
                colour = new Colour(0x19, 0x19, 0xA6); 
            } else {
                colour = calmColour;
            }

            //PrintPath();

            if (canMove) {

                // if (currentPlayerX != previousPlayerX && currentPlayerY != previousPlayerY) // Only recalculate the path if the player has moved
                // {
                switch (type) {
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

                //var directionX = positionX - currentPath[currentPathIndex].x;
                //var directionY = positionY - currentPath[currentPathIndex].y;

                positionX = currentPath[currentPathIndex].x;
                positionY = currentPath[currentPathIndex].y;

                if (currentPathIndex < currentPath.Count) {
                    currentPathIndex++;
                }

                movementTimer = 0;
                canMove = false;
            }
            
            if (movementTimer < timeBetweenMovements) {
                movementTimer += deltaTime;
                canMove = false;
            } else {
                canMove = true;
            }

            if (releaseTimer < releaseTime) {
                releaseTimer += deltaTime;
                canMove = false;
                movementTimer = 0;
            }


        }

        public void Reset() {
            releaseTimer = 0;
            positionX = startX;
            positionY = startY;
        }
        
        private void Move(int deltaX, int deltaY, char[,] maze) {
            int newPosX = positionX + deltaX;
            int newPosY = positionY + deltaY;

            if (maze[newPosY, newPosX] != '▓') {
                positionX = newPosX;
                positionY = newPosY;
            }
        }
        
        // Debug path
        private void PrintPath() {
            if (currentPath != null) {
                foreach (var node in currentPath) {
                    ConsoleRendering.WriteCharAtPoint(node.x, node.y, 'P', new Colour(0, 0, 255), new Colour(255, 255, 255));
                }
            }
        }

        private void SetTargetInky() {
            // Targets a position double that of the distance between blinky and pacman
            targetX = currentPlayerX;
            targetY = currentPlayerY;
            
            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
            // while (currentPath == null) {
            //     currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
            // }

            currentPathIndex = 1;
        }
        
        private void SetTargetBlinky() {
            // Targets the players position directly
            if (!isRunning) {
                targetX = currentPlayerX;
                targetY = currentPlayerY;
            } else {
                // target the top left corner
                targetX = 1;
                targetY = 1;
            }

            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));

            currentPathIndex = 1;
            
        }

        private void SetTargetPinky() {
            // Targets 2 Dots in front of the player
            int playerDirectionX = currentPlayerX - previousPlayerX;
            int playerDirectionY = currentPlayerY - previousPlayerY;

            if (playerDirectionX < 0) {
                targetX = currentPlayerX - 2;
            } else if (playerDirectionX > 0) {
                targetX = currentPlayerX + 2;
            } else {
                targetX = currentPlayerX;
            }

            if (playerDirectionY < 0) {
                targetY = currentPlayerY - 2;
            } else if (playerDirectionX > 0) {
                targetY = currentPlayerY + 2;
            } else {
                targetY = currentPlayerY;
            }
            
            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
            currentPathIndex = 1;
        }

        private void SetTargetClyde() {
            // Targets the players position directly
            targetX = currentPlayerX;
            targetY = currentPlayerY;
            
            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));

            currentPathIndex = 1;
        }
        
        
    }
}
