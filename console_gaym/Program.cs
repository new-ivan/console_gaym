
using console_gaym;

internal class Program
{
    private static async Task Main(string[] args)
    {
        StartMenu startMenu = new StartMenu();
        StartMenu.MenuStatus status = startMenu.ShowMenu();
        GC.Collect();
        GC.SuppressFinalize(startMenu);
        if(status == StartMenu.MenuStatus.status_forceOut)
            return;
        if (status == StartMenu.MenuStatus.status_startGame)
        {
            Game game = new Game();
            await game.Start();
        }
        Console.WriteLine("Game ended");
    }
}