using System.Text;

namespace ConsoleUI.Visuals;
public class TextBlock
{
    /// <summary>
    /// The content of the text block, split into lines.
    /// </summary>
    public string[] Content;

    /// <summary>
    /// The position of the text block.
    /// </summary>
    public (int x, int y) Position;

    /// <summary>
    /// The size of the text block.
    /// </summary>
    public (int w, int h) Size;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBlock"/> class.
    /// </summary>
    /// <param name="content">The content of the text block.</param>
    /// <param name="position">The position of the text block.</param>
    public TextBlock(string content = "", (int x, int y) position = default)
    {
        Content = content.Split('\n');
        Position = position;
        int maxWidth = 0;
        foreach (string r in Content)
        {
            maxWidth = Math.Max(maxWidth, r.Length);
        }
        Size = (maxWidth, Content.Length);
    }

    /// <summary>
    /// Adds a rectangle to the text block.
    /// </summary>
    /// <param name="symbol">The symbol to use for the rectangle.</param>
    /// <param name="position">The position of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public void AddRectangle(char symbol, (int x, int y) position, (int w, int h) size)
    {
        var result = new StringBuilder();

        for (int row = 0; row < size.h; row++)
        {
            result.Append(new string(symbol, size.w));
            if (row < size.h - 1)
            {
                result.Append('\n');
            }
        }

        var toAdd = new TextBlock(result.ToString(), position);
        Add(toAdd);
    }

    /// <summary>
    /// Adds another text block to this text block.
    /// </summary>
    /// <param name="other">The other text block to add.</param>
    /// <returns>True if the addition was successful, otherwise false.</returns>
    public bool Add(TextBlock other)
    {
        for (int r = 0; r < other.Content.Length; r++)
        {
            if (other.Position.y + r >= Content.Length) break;

            var currentRow = new StringBuilder(Content[other.Position.y + r]);
            for (int c = 0; c < other.Content[r].Length; c++)
            {
                if (other.Position.x + c >= currentRow.Length) break;
                currentRow[other.Position.x + c] = other.Content[r][c];
            }
            Content[other.Position.y + r] = currentRow.ToString();
        }
        return true;
    }

    /// <summary>
    /// Creates an empty canvas of the specified size.
    /// </summary>
    /// <param name="size">The size of the canvas.</param>
    /// <returns>A new empty text block representing the canvas.</returns>
    public static TextBlock EmptyCanvas((int w, int h) size)
    {
        var result = new StringBuilder();

        for (int row = 0; row < size.h; row++)
        {
            result.Append(new string(' ', size.w));
            if (row < size.h - 1)
            {
                result.Append('\n');
            }
        }

        return new TextBlock(result.ToString(), (0, 0));
    }

    /// <summary>
    /// Returns a string representation of the text block.
    /// </summary>
    /// <returns>A string representing the text block.</returns>
    public override string ToString()
    {
        return string.Join('\n', Content);
    }

    /// <summary>
    /// Creates a clone of the text block.
    /// </summary>
    /// <returns>A new text block that is a clone of this text block.</returns>
    public TextBlock Clone()
    {
        return new TextBlock(ToString(), Position);
    }
}