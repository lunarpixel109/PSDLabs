namespace ConsoleRenderingHelper;

public static class ConsoleRendering {

    /// <summary>
    /// Writes a char to a point on the screen
    /// </summary>
    /// <param name="left">The X coordinate of the char, starting from the left most point of the screen</param>
    /// <param name="top">The Y coordinate of the char, starting from the right most point of the screen</param>
    /// <param name="c">The char to draw</param>
    /// <param name="foregroundColour"></param>
    /// <param name="backgroundColour"></param>
    public static void WriteCharAtPoint(int left, int top, char c, Colour foregroundColour, Colour backgroundColour) {
        string foregroundAnsi = $"\e[38;2;{foregroundColour.R};{foregroundColour.G};{foregroundColour.B}m";
        string backgroundAnsi = $"\e[48;2;{backgroundColour.R};{backgroundColour.G};{backgroundColour.B}m";
        
        Console.SetCursorPosition(left, top);
        Console.Write($"{foregroundAnsi}{backgroundAnsi}{c}");
    }

    /// <summary>
    /// Draws an array of chars to the console with the top left corner at position (offsetLeft, offsetTop)
    /// </summary>
    /// <param name="offsetLeft">The x coordinate of the array</param>
    /// <param name="offsetTop">The y coordinate of the array</param>
    /// <param name="charGrid">The array to draw</param>
    public static void DrawCharGrid(int offsetLeft, int offsetTop, char[,] charGrid, Colour foregroundColour, Colour backgroundColour) {
        for (int y = 0; y < charGrid.GetLength(0); y++)
        {
            for (int x = 0; x < charGrid.GetLength(1); x++)
            {
                WriteCharAtPoint(offsetLeft + x, offsetTop + y, charGrid[y, x], foregroundColour, backgroundColour);
            }
        }
    }
    
}