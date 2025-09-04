namespace TpCommandManagerMain.MenuManager;

public static class GetUserEntry
{

    public static string? GetString(string message)
    {
        Console.WriteLine(message);
        var chaine = Console.ReadLine();
        return chaine;
    }

    public static int GetEntier(string message)
    {
        Console.WriteLine(message);
        var chaine = Console.ReadLine();
        if (!int.TryParse(chaine, out var sortie))
            sortie = 0;

        return sortie;
    }
}
