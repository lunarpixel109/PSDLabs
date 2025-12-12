using GameCode;
using HighScoreLibrary;
using ConsoleRenderingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects {
    internal class HUD: GameObject {

        MazeGame maze;

        int score;
        PlayerScore highScore;

        private bool powerPelletActive = false;
        private float powerPelletDuration = 10.0f;
        private float powerPelletTimer = 0.0f;

        public HUD(int mazeEndY, MazeGame game): base(0, mazeEndY + 1, new Colour(255, 255, 255), ' ') {
            maze = game;
            maze.highScoreManager.LoadHighScores();
            if (maze.highScoreManager.highScores.Count == 0) {
                highScore = new PlayerScore();
                highScore.score = 0;
                highScore.name = "No High Score";
            } else {
                highScore = maze.highScoreManager.highScores[0];
            }
        }

        public void StartCountdown() {
            powerPelletActive = true;
            powerPelletTimer = powerPelletDuration;

        }


        public override void Update(char[,] maze, ConsoleKey inputKey, float deltaTime) {
            score = this.maze.GetScore();

            if (powerPelletActive) {
                if (powerPelletTimer > 0) {
                    powerPelletTimer -= deltaTime;
                } else {
                    powerPelletActive = false;
                    powerPelletTimer = 0.0f;
                }
            }
        }

        public override void Draw(char[,] maze) {
            ConsoleRendering.WriteString(0, positionY, $"Current Score\t{score}", new Colour(0, 0, 0), colour);
            ConsoleRendering.WriteString(0, positionY + 1, $"High Score\t{highScore.getScore()} by {highScore.getName()}", new Colour(0, 0, 0), colour);

            if (powerPelletActive) {
                ConsoleRendering.WriteString(0, positionY + 2, $"Power Pellet Active! {powerPelletTimer:0.0}s remaining", new Colour(255, 0, 0), colour);
            } else {
                ConsoleRendering.WriteString(0, positionY + 2, $"                                                      ", new Colour(0, 0, 0), new Colour(0, 0, 0));
            }
        }
    }
}
