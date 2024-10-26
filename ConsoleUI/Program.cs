using ConsoleUI.Logic;
using ConsoleUI.Visuals;

namespace ConsoleUI;

/// <summary>
/// The main entry point for the application.
/// </summary>
class Program
{
	/// <summary>
	/// The main method, which is the entry point of the application.
	/// </summary>
	/// <param name="args">The command-line arguments.</param>
	public static void Main(string[] args)
	{
		IUserInterface userInterface = new UserInterface();
		var game = new Game();
		while (true)
		{
			try
			{
				userInterface.Draw(game);
			}
			catch
			{
				// ignored
			}
			string? command = userInterface.GetCommand();
			if (command is "q") break;
			game.Update(command);
			Thread.Sleep(1000 / 60);
		}
	}
}