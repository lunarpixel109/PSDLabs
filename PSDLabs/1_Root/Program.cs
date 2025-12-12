using ConsoleRenderingHelper;
using GameCode;
using System;

class Program {

    public static Colour HueToRGB(float hue) {
        // Ensure hue is between 0 and 1
        hue = hue % 1.0f;
        if (hue < 0) hue += 1.0f;

        // Standard HSV to RGB conversion algorithm (assuming Saturation=1, Value=1 for max brightness)
        float h = hue * 6f;
        float x = 1f - Math.Abs((h % 2f) - 1f);

        float r = 0, g = 0, b = 0;

        if (h < 1f) { r = 1f; g = x; b = 0f; } else if (h < 2f) { r = x; g = 1f; b = 0f; } else if (h < 3f) { r = 0f; g = 1f; b = x; } else if (h < 4f) { r = 0f; g = x; b = 1f; } else if (h < 5f) { r = x; g = 0f; b = 1f; } else { r = 1f; g = 0f; b = x; }

        // Scale to 0-255 integers
        return new Colour((int)(r * 255), (int)(g * 255), (int)(b * 255));
    }

    public static void PlayPacmanTheme() {

        Console.Beep(440, 167);
        Console.Beep(880, 167);
        Console.Beep(660, 167);
        Console.Beep(554, 167);
        Console.Beep(880, 167);
        Console.Beep(660, 167);
        Console.Beep(554, 333);
        Console.Beep(466, 167);
        Console.Beep(932, 167);
        Console.Beep(698, 167);
        Console.Beep(478, 167);
        Console.Beep(932, 167);
        Console.Beep(698, 167);
        Console.Beep(478, 333);
        Console.Beep(440, 167);
        Console.Beep(880, 167);
        Console.Beep(660, 167);
        Console.Beep(554, 167);
        Console.Beep(880, 167);
        Console.Beep(660, 167);
        Console.Beep(554, 333);
        Console.Beep(554, 83);
        Console.Beep(587, 83);
        Console.Beep(622, 167);
        Console.Beep(622, 83);
        Console.Beep(659, 83);
        Console.Beep(698, 167);
        Console.Beep(698, 83);
        Console.Beep(830, 83);
        Console.Beep(880, 333);

    }
    
    static void Main(string[] args) {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string[] pacmanLogo = new string[]
        {
             @" ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄       ▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄        ▄ ",
             @"▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░▌     ▐░░▌▐░░░░░░░░░░░▌▐░░▌      ▐░▌",
             @"▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░▌░▌   ▐░▐░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌░▌     ▐░▌",
             @"▐░▌       ▐░▌▐░▌       ▐░▌▐░▌          ▐░▌▐░▌ ▐░▌▐░▌▐░▌       ▐░▌▐░▌▐░▌    ▐░▌",
             @"▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌▐░▌          ▐░▌ ▐░▐░▌ ▐░▌▐░█▄▄▄▄▄▄▄█░▌▐░▌ ▐░▌   ▐░▌",
             @"▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌          ▐░▌  ▐░▌  ▐░▌▐░░░░░░░░░░░▌▐░▌  ▐░▌  ▐░▌",
             @"▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░▌          ▐░▌   ▀   ▐░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌   ▐░▌ ▐░▌",
             @"▐░▌          ▐░▌       ▐░▌▐░▌          ▐░▌       ▐░▌▐░▌       ▐░▌▐░▌    ▐░▌▐░▌",
             @"▐░▌          ▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░▌       ▐░▌▐░▌       ▐░▌▐░▌     ▐░▐░▌",
             @"▐░▌          ▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░▌      ▐░░▌",
             @" ▀            ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀         ▀  ▀        ▀▀ "
        };

        int totalRows = pacmanLogo.Length;
        int maxCols = pacmanLogo.Max(s => s.Length);

        float maxCoordinateSum = (float)(totalRows + maxCols);

        float rainbowFrequency = 1.2f;
        float hueOffset = 0.0f;
        // ---------------------------

        for (int row = 0; row < totalRows; row++) {
            string currentLine = pacmanLogo[row];
            for (int col = 0; col < currentLine.Length; col++) {
                char currentChar = currentLine[col];

                // --- THE DIAGONAL LOGIC ---
                // We calculate how far "along" the diagonal we are.
                float currentCoordinateSum = col + row;

                // Normalize this position to a 0.0 -> 1.0 range
                float normalizedPosition = currentCoordinateSum / maxCoordinateSum;

                // Apply frequency and offset tweaks
                float finalHue = (normalizedPosition * rainbowFrequency) + hueOffset;

                // Generate the RGB colour for this specific character position
                Colour characterColour = HueToRGB(finalHue);
                // --------------------------

                // Call your rendering function
                ConsoleRendering.WriteCharAtPoint(col, row, currentChar, characterColour, new Colour(0, 0, 0));
            }
            // End of row newline
            Console.WriteLine();
        }

        PlayPacmanTheme();

        Console.WriteLine("Press any key to start the game...");
        Console.ReadKey(true);

    StartGame:
        MazeGame game = new MazeGame();
        game.Run();
        
        Console.WriteLine("Press R to retry or press any other key to exit");
        var key = Console.ReadKey(true);
        switch (key.Key) {
            case ConsoleKey.Escape:
                return;
            case ConsoleKey.R:
                game = null;
                goto StartGame;
        }
        
        
    }
}
