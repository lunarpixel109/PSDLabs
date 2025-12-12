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

        public int pinkyTargetX;
        public int pinkyTargetY;

        public int blinkyTargetX;
        public int blinkyTargetY;

        public void UpdatePlayerPosition(int x, int y) {
            
            previousPlayerX = currentPlayerX;
            previousPlayerY = currentPlayerY;
            
            currentPlayerX = x;
            currentPlayerY = y;
        }
        
        public void SetRunningState(bool running) {
            isRunning = running;
        }

        public Ghost(int startX, int startY, Colour colour, float timeBetweenMoves, float releaseTime, GhostType ghostType) : base(startX, startY, colour, '▀') {
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
            // Scatter mode: Inky heads to its scatter corner.
            // Chase mode (normal): classic behaviour using Pinky's two-tiles-ahead and Blinky's target (published via SetTargetBlinky(true)).
            if (isRunning) {
                // Scatter -> bottom-right corner (matching pellets placed in MazeGame)
                targetX = 25;
                targetY = 20;
            } else {
                // Compute components using the helper functions in targetSelect mode
                SetTargetPinky(true);   // sets pinkyTargetX / pinkyTargetY
                SetTargetBlinky(true);  // sets blinkyTargetX / blinkyTargetY

                // If Blinky's published values are not initialized (0,0) and match player start,
                // keep a safe fallback to chase the player.
                targetX = pinkyTargetX * 2 - blinkyTargetX;
                targetY = pinkyTargetY * 2 - blinkyTargetY;
            }

            var maze = MazeGame.Maze;
            if (maze != null) {
                targetX = Math.Clamp(targetX, 0, maze.GetLength(1) - 1);
                targetY = Math.Clamp(targetY, 0, maze.GetLength(0) - 1);
            }

            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
            if (currentPath == null || currentPath.Count < 2) {
                // fallback: if we couldn't find a path to the computed target, chase the player
                currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(currentPlayerX, currentPlayerY));
            }

            if (currentPath != null && currentPath.Count > 1) {
                currentPathIndex = 1;
            } else if (currentPath != null && currentPath.Count == 1) {
                currentPathIndex = 0;
            } else {
                currentPath = new List<Node>() { new Node(positionX, positionY) };
                currentPathIndex = 0;
            }
        }

        private void SetTargetBlinky(bool targetSelect = false) {
            // In scatter mode Blinky aims for his corner; otherwise chase player.
            if (isRunning) {
                // Scatter corner for Blinky -> top-right
                targetX = 25;
                targetY = 1;
            } else {
                targetX = currentPlayerX;
                targetY = currentPlayerY;
            }

            if (targetSelect) {
                blinkyTargetX = targetX;
                blinkyTargetY = targetY;
            } else {
                currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
                if (currentPath != null && currentPath.Count > 1) {
                    currentPathIndex = 1;
                } else if (currentPath != null && currentPath.Count == 1) {
                    currentPathIndex = 0;
                } else {
                    currentPath = new List<Node>() { new Node(positionX, positionY) };
                    currentPathIndex = 0;
                }
            }

        }

        private void SetTargetPinky(bool targetSelect = false) {
            // Scatter mode: Pinky -> top-left corner
            if (isRunning) {
                targetX = 1;
                targetY = 1;
            } else {
                // Targets 2 tiles in front of the player
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
                } else if (playerDirectionY > 0) { // fixed: check Y
                    targetY = currentPlayerY + 2;
                } else {
                    targetY = currentPlayerY;
                }
            }

            if (targetSelect) {
                pinkyTargetX = targetX;
                pinkyTargetY = targetY;
            } else {
                currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
                if (currentPath != null && currentPath.Count > 1) {
                    currentPathIndex = 1;
                } else if (currentPath != null && currentPath.Count == 1) {
                    currentPathIndex = 0;
                } else {
                    currentPath = new List<Node>() { new Node(positionX, positionY) };
                    currentPathIndex = 0;
                }
            }


        }

        private void SetTargetClyde() {
            // Scatter mode: Clyde -> bottom-left corner
            if (isRunning) {
                targetX = 1;
                targetY = 20;
            } else {
                // Targets the player's position directly, but if too close, head to bottom-left
                targetX = currentPlayerX;
                targetY = currentPlayerY;

                if (Distance(currentPlayerX, currentPlayerY, positionX, positionY) < 8) {
                    // Target bottom-left corner
                    targetX = 1;
                    targetY = 20;
                }
            }

            currentPath = Pathfinding.FindPath(new Node(positionX, positionY), new Node(targetX, targetY));
            if (currentPath != null && currentPath.Count > 1) {
                currentPathIndex = 1;
            } else if (currentPath != null && currentPath.Count == 1) {
                currentPathIndex = 0;
            } else {
                currentPath = new List<Node>() { new Node(positionX, positionY) };
                currentPathIndex = 0;
            }
        }

        private int Distance(int x1, int y1, int x2, int y2) {
            return (int)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        
    }
}
