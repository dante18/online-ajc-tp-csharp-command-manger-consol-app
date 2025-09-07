using System.Globalization;

namespace TpCommandManagerMain.MenuManager;

public static class GetUserEntry
{

    private static List<string> _trueValues = [
        "oui",
        "o",
        "yes",
        "y"
        ];

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

    public static bool GetBool(string message)
    {
        Console.WriteLine(message);
        var chaine = Console.ReadLine();
        return _trueValues.Contains(chaine, StringComparer.OrdinalIgnoreCase);
    }

    public static DateTime GetDate(string message)
    {
        while (true)
        {
            Console.Write($"{message} (JJ/MM/AAAA) : ");
            string? saisie = Console.ReadLine();

            if (DateTime.TryParseExact(saisie, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var date))
            {
                return date;
            }

            Console.WriteLine("Format de la date invalide, réessayez !");
        }
    }
}
