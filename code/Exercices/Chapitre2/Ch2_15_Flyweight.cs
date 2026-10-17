// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/15. Flyweight.md

namespace CoursPOO.Exercices.Ch2_15_Flyweight;

public sealed class Espece
{
    public string Nom { get; }
    public string Couleur { get; }
    public Espece(string nom, string couleur) { Nom = nom; Couleur = couleur; }
}

public class Arbre
{
    public Espece Espece { get; }
    public int X { get; }
    public int Y { get; }
    public Arbre(Espece espece, int x, int y) { Espece = espece; X = x; Y = y; }
}

public class Foret
{
    private readonly Dictionary<(string, string), Espece> especes = new();
    private readonly List<Arbre> arbres = new();

    public int NombreArbres => arbres.Count;
    public int NombreEspeces => especes.Count;

    public void Planter(int x, int y, string nom, string couleur)
    {
        if (!especes.TryGetValue((nom, couleur), out var espece))
        {
            espece = new Espece(nom, couleur);
            especes[(nom, couleur)] = espece;
        }
        arbres.Add(new Arbre(espece, x, y));
    }

    public void Dessiner()
    {
        foreach (var arbre in arbres)
            Console.WriteLine($"{arbre.Espece.Nom} ({arbre.Espece.Couleur}) en ({arbre.X}, {arbre.Y})");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var foret = new Foret();
        foret.Planter(0, 0, "Chêne", "vert");
        foret.Planter(3, 1, "Sapin", "vert foncé");
        foret.Planter(5, 2, "Chêne", "vert");
        foret.Planter(7, 4, "Chêne", "roux");
        foret.Planter(9, 9, "Sapin", "vert foncé");
        foret.Planter(2, 8, "Chêne", "vert");

        foret.Dessiner();
        Console.WriteLine($"{foret.NombreArbres} arbres, {foret.NombreEspeces} espèces en mémoire");
    }
}
