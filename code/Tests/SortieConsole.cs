using System.Text.RegularExpressions;

// Les tests redirigent Console.Out, qui est global : ils ne doivent pas s’exécuter en parallèle.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace CoursPOO.Tests;

/// <summary>Capture ce qu’un scénario écrit dans la console et le compare à la sortie attendue.</summary>
public static class SortieConsole
{
    public static string Capturer(Action scenario)
    {
        var original = Console.Out;
        var tampon = new StringWriter();
        Console.SetOut(tampon);
        try
        {
            scenario();
        }
        finally
        {
            Console.SetOut(original);
        }
        return Normaliser(tampon.ToString());
    }

    public static void Verifier(Action scenario, string attendu, bool normaliserNumeros = false)
    {
        string obtenu = Capturer(scenario);
        attendu = Normaliser(attendu);

        if (normaliserNumeros)
        {
            // « Marine #3 » devient « Marine #n » : la numérotation dépend des tests déjà exécutés
            obtenu = Regex.Replace(obtenu, @"#\d+", "#n");
            attendu = Regex.Replace(attendu, @"#\d+", "#n");
        }

        Assert.Equal(attendu, obtenu);
    }

    private static string Normaliser(string texte) => texte.Replace("\r\n", "\n").TrimEnd('\n');
}
