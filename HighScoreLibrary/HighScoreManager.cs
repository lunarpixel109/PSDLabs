using System.Data.SQLite;
using ConsoleRenderingHelper;
namespace HighScoreLibrary;
public class HighScoreManager {
    
    SQLiteConnection sqliteConnection;

    private string currentPlayername;

    
    public HighScoreManager() {
        sqliteConnection = new SQLiteConnection("Data Source=highscore.db;Version=3;");
        try {
            sqliteConnection.Open(); 
        } catch (Exception e) {
            Console.WriteLine(e.Message);
            Console.ReadKey(true);
        }
    }


    public void LoginPlayer() {
        Console.Clear();
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        SQLiteDataReader SQLite_DataReader;
        SQLiteCommand sqliteCommand;
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = $"SELECT * FROM player WHERE username = \'{name}\'";
        SQLite_DataReader = sqliteCommand.ExecuteReader();
        if (SQLite_DataReader.HasRows) {
            currentPlayername = name;
        } else {
            var sqliteCommand2 = sqliteConnection.CreateCommand();
            sqliteCommand2.CommandText = $"INSERT INTO player(`username`) VALUES (\'{name}\')";
            if (sqliteCommand2.ExecuteNonQuery() == 0) {
                Console.WriteLine("Player already exists, but hasnt been logged in, something went wrong.");
                throw new Exception("Player already exists, but hasnt been logged in.");
            }
        }
    }

    public void AddScore(int score) {
        SQLiteCommand sqliteCommand;
        
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = $"INSERT INTO run(player, score, datetime) VALUES (\'{currentPlayername}\', \'{score}\', \'{DateTime.Now:yyyy-MM-dd HH:mm:ss}\')";
        sqliteCommand.ExecuteNonQuery();

        if (sqliteCommand.ExecuteNonQuery() == 0) {
            Console.WriteLine("Failed to write score");
            throw new Exception("Player write score failed");
        }
    }

    public void DisplayScores() {
        Console.Clear();
        ConsoleRendering.WriteLine($"Scores for player {currentPlayername}:", new Colour(255, 255, 255), new Colour(0, 143, 138));
        
        SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "SELECT score, datetime FROM run WHERE player = \'{currentPlayername}\' ORDER BY score DESC";
        SQLiteDataReader SQLite_DataReader = sqliteCommand.ExecuteReader();
        int i = 0;
        while (SQLite_DataReader.Read() && i < 10) {
            string score = SQLite_DataReader.GetString(0);
            string datetime =  SQLite_DataReader.GetString(1);
            
            ConsoleRendering.WriteLine($"Scored {score} on {datetime}", new Colour (0, 0, 0), new Colour(255, 255, 255));
            i++;
        }
        Console.WriteLine("\n");
        ConsoleRendering.WriteLine("Top Scores Overall", new Colour(255, 255, 255), new Colour(0, 143, 138));
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "SELECT player, score, datetime FROM run ORDER BY score DESC";
        SQLite_DataReader = sqliteCommand.ExecuteReader();
        i = 0;
        while (SQLite_DataReader.Read() || i < 10) {
            var playerID = SQLite_DataReader.GetString(0);
            var score = SQLite_DataReader.GetInt32(1);
            var datetime =  SQLite_DataReader.GetDateTime(2);
            
            ConsoleRendering.WriteLine($"{playerID} scored {score} on {datetime}", new Colour (0, 0, 0), new Colour(255, 255, 255));
            i++;
        }
    }

    public void EndOfGame(int score) {
        AddScore(score);
        DisplayScores();
    }
    
    
}