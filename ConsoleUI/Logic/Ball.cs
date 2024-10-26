namespace ConsoleUI.Logic;

/// <summary>
/// Represents a ball in the game, which is a type of game object.
/// </summary>
public class Ball : GameObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Ball"/> class.
	/// </summary>
	/// <param name="startingPosition">The starting position of the ball as a tuple (x, y).</param>
	/// <param name="radius">The radius of the ball.</param>
	public Ball((double x, double y) startingPosition, double radius)
		: base(startingPosition, (radius * 2, radius * 2))
	{
		Velocity = (0.025, 0.01);
	}

	/// <summary>
	/// Updates the position of the ball based on its velocity and handles boundary collisions.
	/// </summary>
	public override void Update()
	{
		// Update the position of the ball based on its current velocity.
		Position = (Position.x + Velocity.x, Position.y + Velocity.y);

		// Check for collisions with the horizontal boundaries and invert the x-velocity if necessary.
		if (Position.x < 0 || Position.x > 1 - Size.w)
		{
			Velocity = (-Velocity.x, Velocity.y);
		}

		// Check for collisions with the vertical boundaries and invert the y-velocity if necessary.
		if (Position.y < 0 || Position.y > 1 - Size.h)
		{
			Velocity = (Velocity.x, -Velocity.y);
		}
	}
}