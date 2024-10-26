namespace ConsoleUI.Logic;

/// <summary>
/// Represents an abstract game object with position, size, and velocity properties.
/// </summary>
public abstract class GameObject
{
	/// <summary>
	/// Gets or sets the position of the game object as a tuple (x, y).
	/// </summary>
	public (double x, double y) Position { get; set; }

	/// <summary>
	/// Gets or sets the size of the game object as a tuple (w, h).
	/// </summary>
	public (double w, double h) Size { get; set; }

	/// <summary>
	/// Gets or sets the velocity of the game object as a tuple (x, y).
	/// </summary>
	public (double x, double y) Velocity { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="GameObject"/> class.
	/// </summary>
	/// <param name="startingPosition">The starting position of the game object as a tuple (x, y).</param>
	/// <param name="size">The size of the game object as a tuple (w, h).</param>
	public GameObject((double x, double y) startingPosition, (double w, double h) size)
	{
		Position = startingPosition;
		Size = size;
		Velocity = (0, 0);
	}

	/// <summary>
	/// Updates the state of the game object. Must be implemented by derived classes.
	/// </summary>
	public abstract void Update();

	/// <summary>
	/// Determines whether this game object intersects with another game object.
	/// </summary>
	/// <param name="other">The other game object to check for intersection.</param>
	/// <returns><c>true</c> if the game objects intersect; otherwise, <c>false</c>.</returns>
	public bool Intersects(GameObject other)
	{
		return Position.x < other.Position.x + other.Size.w &&
		       Position.x + Size.w > other.Position.x &&
		       Position.y < other.Position.y + other.Size.h &&
		       Position.y + Size.h > other.Position.y;
	}
}