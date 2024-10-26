namespace ConsoleUI.Logic;
public class Game
{
	/// <summary>
	/// Gets the collection of game objects in the game.
	/// </summary>
	public Dictionary<string, GameObject> GameObjects { get; private set; } = new Dictionary<string, GameObject>
	{
		{ "ball", new Ball((0.5, 0.5), 0.02) },
		{ "paddleLeft", new Paddle((0.025, 0.5), (0.025, 0.2)) },
		{ "paddleRight", new Paddle((0.95, 0.5), (0.025, 0.2)) }
	};

	/// <summary>
	/// Initializes a new instance of the <see cref="Game"/> class.
	/// </summary>
	public Game()
	{
	}

	/// <summary>
	/// Updates the game state based on the provided input.
	/// </summary>
	/// <param name="input">The input command to process.</param>
	public void Update(string? input)
	{
		if (!string.IsNullOrEmpty(input))
		{
			var commands = input.Split(' ');
			var gameObject = GameObjects[commands[0]];
			gameObject.Position = gameObject.Position with
			{
				y = gameObject.Position.y + (commands[1] == "up" ? -0.1 : 0.1)
			};
		}

		foreach (var gameObject in GameObjects.Values)
		{
			gameObject.Update();
		}

		var ball = GameObjects["ball"];
		if (ball.Intersects(GameObjects["paddleLeft"]) || ball.Intersects(GameObjects["paddleRight"]))
		{
			ball.Velocity = ball.Velocity with { x = ball.Velocity.x * -1 };
		}
	}
}