using System.Globalization;

namespace TpCommandManagerConsole.Input;

public static class GetUserEntry
{
    private static readonly List<string> TrueValues = [
        "oui",
        "o",
        "yes",
        "y"
        ];

    public static string? GetString(string message)
    {
        Console.WriteLine(message);
        var userEntry = Console.ReadLine();

        return userEntry;
    }

    public static int GetEntier(string message)
    {
        Console.WriteLine(message);
        var userEntry = Console.ReadLine();

        if (!int.TryParse(userEntry, out var outResult))
            outResult = 0;

        return outResult;
    }

    public static decimal GetDecimal(string message)
    {
        Console.WriteLine(message);
        var userEntry = Console.ReadLine();

        if (!decimal.TryParse(userEntry, new CultureInfo("fr-FR"), out var outResult))
            outResult = 0;

        return outResult;
    }

    public static bool GetBool(string message)
    {
        Console.WriteLine(message);
        var userEntry = Console.ReadLine();

        return TrueValues.Contains(userEntry, StringComparer.OrdinalIgnoreCase);
    }

    public static DateTime GetDate(string message)
    {
        while (true)
        {
            Console.Write($"{message} (JJ/MM/AAAA) : ");
            string? userEntry = Console.ReadLine();

            if (DateTime.TryParseExact(userEntry, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var outResult))
            {
                return outResult;
            }

            Console.WriteLine("Invalid date format, try again!");
        }
    }
}
