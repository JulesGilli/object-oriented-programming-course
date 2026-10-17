// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie I — Patterns Créationnels/1. Singleton.md

namespace CoursPOO.Exercices.Ch2_01_Singleton;

public sealed class ConfigurationJeu
{
    private static readonly Lazy<ConfigurationJeu> instance =
        new Lazy<ConfigurationJeu>(() => new ConfigurationJeu());

    public static ConfigurationJeu Instance => instance.Value;
    public static int NombreInstances { get; private set; }

    public int Volume { get; set; } = 100;

    private ConfigurationJeu()
    {
        NombreInstances++;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        Console.WriteLine($"Instances avant le premier accès : {ConfigurationJeu.NombreInstances}");

        var a = ConfigurationJeu.Instance;
        var b = ConfigurationJeu.Instance;
        a.Volume = 80;

        Console.WriteLine($"Même objet : {ReferenceEquals(a, b)}");
        Console.WriteLine($"Volume vu par b : {b.Volume}");
        Console.WriteLine($"Instances après : {ConfigurationJeu.NombreInstances}");
    }
}
