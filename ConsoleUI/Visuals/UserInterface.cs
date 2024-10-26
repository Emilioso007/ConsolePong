using ConsoleUI.Logic;

namespace ConsoleUI.Visuals;
public class UserInterface : IUserInterface
{
    /// <summary>
    /// Gets the current canvas for drawing.
    /// </summary>
    public TextBlock Canvas { get; private set; }

    /// <summary>
    /// Gets the previous canvas for comparison.
    /// </summary>
    public TextBlock PrevCanvas { get; private set; }

    private (int w, int h) lastConsole = (0, 0);

    /// <summary>
    /// Initializes a new instance of the <see cref="UserInterface"/> class.
    /// </summary>
    public UserInterface()
    {
        var canvasDimensions = (Console.WindowWidth, Console.WindowHeight - 1);
        Canvas = TextBlock.EmptyCanvas(canvasDimensions);
        PrevCanvas = new TextBlock();
    }

    /// <summary>
    /// Draws the game objects on the canvas.
    /// </summary>
    /// <param name="game">The game instance containing game objects to draw.</param>
    public void Draw(Game game)
    {
	    (int w, int h) canvasDimensions = (Console.WindowWidth, Console.WindowHeight - 1);
        Canvas = TextBlock.EmptyCanvas(canvasDimensions);
        Canvas.AddRectangle(' ', (0, 0), canvasDimensions);

        foreach (var kvp in game.GameObjects)
        {
            char symbol = kvp.Key switch
            {
                "ball" => 'B',
                "paddleLeft" or "paddleRight" => 'P',
                _ => '?'
            };

            var gameObject = kvp.Value;
            Canvas.AddRectangle(symbol,
                MapUnitTupleToRange(gameObject.Position, canvasDimensions),
                MapUnitTupleToRange(gameObject.Size, canvasDimensions));
        }

        if ((Console.WindowWidth, Console.WindowHeight).Equals(lastConsole))
        {
            for (int r = 0; r < Canvas.Size.h; r++)
            {
                for (int c = 0; c < Canvas.Size.w; c++)
                {
                    if (Canvas.Content[r][c] != PrevCanvas.Content[r][c])
                    {
                        try
                        {
                            Console.SetCursorPosition(c, r);
                            Console.Write(Canvas.Content[r][c]);
                        }
                        catch
                        {
	                        // ignored
                        }
                    }
                }
            }
            Console.SetCursorPosition(0, canvasDimensions.h);
        }
        else
        {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.WriteLine(Canvas.ToString());
        }
        PrevCanvas = Canvas.Clone();
        lastConsole = (Console.WindowWidth, Console.WindowHeight);
    }

    /// <summary>
    /// Gets the user command based on key input.
    /// </summary>
    /// <returns>A string representing the user command.</returns>
    public string GetCommand()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.W => "paddleLeft up",
                ConsoleKey.S => "paddleLeft down",
                ConsoleKey.UpArrow => "paddleRight up",
                ConsoleKey.DownArrow => "paddleRight down",
                _ => string.Empty
            };
        }
        return string.Empty;
    }

    /// <summary>
    /// Maps a tuple of unit coordinates to a range.
    /// </summary>
    /// <param name="unitTuple">The unit coordinates to map.</param>
    /// <param name="range">The range to map to.</param>
    /// <returns>A tuple representing the mapped coordinates.</returns>
    private (int x, int y) MapUnitTupleToRange((double x, double y) unitTuple, (int x, int y) range)
    {
        return ((int)(range.x * unitTuple.x), (int)(range.y * unitTuple.y));
    }
}