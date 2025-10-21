using System;
using System.IO;
using System.Numerics;


namespace HighScoreLibrary
{
    struct Player 
    {
        public string name;
        public int score;

        public void setScore(int s) { score = s; }
        public int getScore() { return score; }

        public void setName(string n) { name = n; }
        public string getName() { return name; }

    }

    public class HighScoreManager
    {
        private readonly string HighScoreFile;

        public HighScoreManager(string highScoreFilePath)
        {
            HighScoreFile = highScoreFilePath;
        }

        public void SaveHighScore(string playerName, int moves)
        {
            using (StreamWriter writer = new StreamWriter(HighScoreFile, true))
            {
                writer.WriteLine($"{playerName},{moves}");
            }
        }

        public void LoadHighScores()
        {
            if (File.Exists(HighScoreFile))
            {
                //DisplayHighScores();
            }
            else
            {
                Console.WriteLine("No high scores yet!");
            }
        }

        public void DisplayHighScores()
        {
            if (File.Exists(HighScoreFile))
            {
                Console.WriteLine("High Scores:");
                using (StreamReader reader = new StreamReader(HighScoreFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            Console.WriteLine($"{parts[0]}: {parts[1]} moves");
                        }
                    }
                }
            }
        }

        public void EndOfGame(int score)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");

            // Ask for the player's name
            Console.Write("Enter your name: ");

            string playerName = Console.ReadLine();

            if (playerName != "")
            {
                SaveHighScore(playerName, score);
            }
            else while (playerName == "") 
            {
                Console.Write("Enter a valid name: ");
                playerName = Console.ReadLine();
                    if (playerName != "")
                    {
                        SaveHighScore(playerName, score);
                    }
            }
            // Save the high score
            

            // Load and display high scores
            LoadHighScores();
        }

        public void SortScores()
        {
            List<Player> players = new List<Player>();

            if (File.Exists(HighScoreFile))
            {
                using (StreamReader reader = new StreamReader(HighScoreFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            Player player = new Player();
                            player.setName(parts[0]);
                            player.setScore(int.Parse(parts[1]));
                            players.Add(player);
                        }
                    }
                }
            }

            // Bubble sort implementation
            // TODO: Switch to merge sort
            // TODO: Switch to merge sort
            var n = players.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (players[j].getScore() > players[j + 1].getScore())
                    {
                        var temp = players[j];
                        players[j] = players[j + 1];
                        players[j + 1] = temp;
                    }

            // Clear the file and save sorted scores
            File.WriteAllText(HighScoreFile, String.Empty);

            foreach (var player in players)
            {
                SaveHighScore(player.getName(), player.getScore());
            }
        }


    }
}
