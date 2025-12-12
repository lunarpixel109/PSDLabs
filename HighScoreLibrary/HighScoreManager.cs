using System;
using System.IO;
using System.Numerics;
using System.Text.Json;
using ConsoleRenderingHelper;


namespace HighScoreLibrary
{
    public class PlayerScore 
    {
        public string name  { get; set; }
        public int    score { get; set; }

        public void setScore(int s) { score = s; }
        public int getScore() { return score; }

        public void setName(string n) { name = n; }
        public string getName() { return name; }

    }

    public class HighScoreManager
    {
        private readonly string HighScoreFile;

        public List<PlayerScore> highScores;

        public HighScoreManager(string highScoreFilePath)
        {
            HighScoreFile = highScoreFilePath;
            highScores = new List<PlayerScore>();
        }

        public void SaveHighScore(string playerName, int moves)
        {
            highScores.Add(new PlayerScore() { name = playerName, score = moves });
        }

        public void LoadHighScores()
        {
            if (File.Exists(HighScoreFile))
            {
                using (StreamReader reader = new StreamReader(HighScoreFile)) {
                    var loadedHighScores = JsonSerializer.Deserialize<List<PlayerScore>>(reader.ReadToEnd());
                    highScores = loadedHighScores == null ? new List<PlayerScore>() : loadedHighScores;
                }
            }
            else
            {
                Console.WriteLine("No high scores yet!");
            }
        }

        public void DisplayHighScores()
        {
            Console.Clear();
            if (File.Exists(HighScoreFile))
            {
                ConsoleRendering.WriteString(0, 0, "High Scores:" , new Colour(0, 0, 0), new Colour(255, 255, 255));
                int currentLine = 0;
                foreach (var player in highScores) {
                    ConsoleRendering.WriteString(0, currentLine+1, $"{player.getName()}: {player.getScore()}", new Colour(255, 255, 255), new Colour(0, 0, 0));
                    currentLine++;
                }
            }
        }

        public void EndOfGame(int score)
        {
            ConsoleRendering.Clear(new Colour(0, 0, 0));
            string[] gameOverText = {
                " ▄████  ▄████▄ ██▄  ▄██ ██████   ▄████▄ ██  ██ ██████ █████▄  ",
                "██  ▄▄▄ ██▄▄██ ██ ▀▀ ██ ██▄▄     ██  ██ ██▄▄██ ██▄▄   ██▄▄██▄ ",
                " ▀███▀  ██  ██ ██    ██ ██▄▄▄▄   ▀████▀  ▀██▀  ██▄▄▄▄ ██   ██ "
            };

            foreach (string line in gameOverText) {
                ConsoleRendering.WriteString(0, Array.IndexOf(gameOverText, line), line, new Colour(255, 0, 0), new Colour(0, 0, 0), true);
            }


            // Ask for the player's name
            ConsoleRendering.WriteString(0, 4, "Enter your name: ", new Colour(255, 255, 255), new Colour(0, 0, 0));

            string playerName = Console.ReadLine();
            
            LoadHighScores();

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
            

        }

        public void SortScores()
        {

            MergeSort(highScores);

            // Clear the file and save sorted scores
            File.WriteAllText(HighScoreFile, String.Empty);

            if (highScores.Count >= 11) {
                highScores.RemoveRange(10, highScores.Count - 10);
            }
            
            string json = JsonSerializer.Serialize(highScores);
            File.WriteAllText(HighScoreFile, json);
        }

        void MergeSort(List<PlayerScore> array) {
            
            int length = array.Count;

            if (length <= 1) return; // Base Case

            int middle = length / 2;
            List<PlayerScore> left = new  List<PlayerScore>(middle);
            List<PlayerScore> right = new  List<PlayerScore>(length - middle);



            for (int i = 0; i < length; i++) {
                if (i < middle) {
                    left.Add(array[i]);
                } else {
                    right.Add(array[i]);
                }
            }

            MergeSort(left);
            MergeSort(right);
            Merge(left, right, array);


        }

        void Merge(List<PlayerScore> leftArray, List<PlayerScore> rightArray, List<PlayerScore> array) {
            
            int leftSize =  array.Count / 2;
            int rightSize = array.Count - leftSize;
            int l = 0, r = 0, i = 0;

            while (l < leftSize && r < rightSize) {
                if (leftArray[l].getScore() > rightArray[r].getScore()) {
                    array[i] = leftArray[l];
                    l++;
                } else {
                    array[i] = rightArray[r];
                    r++;
                }
                i++;
            }

            while (l < leftSize) {
                array[i] = leftArray[l];
                i++;
                l++;
            }
            while (r < rightSize) {
                array[i] = rightArray[r];
                i++;
                r++;
            }

        }
        
    }
}
