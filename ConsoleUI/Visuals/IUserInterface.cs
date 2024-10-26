using ConsoleUI.Logic;

namespace ConsoleUI.Visuals;

/// <summary>
/// Defines the user interface for the game.
/// </summary>
public interface IUserInterface
{
	/// <summary>
	/// Draws the game state on the user interface.
	/// </summary>
	/// <param name="game">The game instance to draw.</param>
	void Draw(Game game);

	/// <summary>
	/// Gets the command input from the user.
	/// </summary>
	/// <returns>A string representing the user's command, or null if no command is available.</returns>
	string? GetCommand();
}