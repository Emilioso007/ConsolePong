namespace ConsoleUI.Logic;

/// <summary>
/// Represents a paddle in the game, which is a type of game object.
/// </summary>
public class Paddle : GameObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Paddle"/> class.
	/// </summary>
	/// <param name="startingPosition">The starting position of the paddle as a tuple (x, y).</param>
	/// <param name="size">The size of the paddle as a tuple (w, h).</param>
	public Paddle((double x, double y) startingPosition, (double w, double h) size)
		: base(startingPosition, size) { }

	/// <summary>
	/// Updates the position of the paddle, ensuring it stays within the vertical bounds of the game area.
	/// </summary>
	public override void Update()
	{
		Position = (Position.x, Math.Clamp(Position.y, 0, 1 - Size.h));
	}
}