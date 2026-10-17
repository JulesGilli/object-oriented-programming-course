// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie IV — Les outils du langage/12. Types valeur et types référence.md

namespace CoursPOO.Exercices.Ch1_12_TypesValeurEtTypesReference;

public struct PointS { public int X; public int Y; } public class Joueur { public string Nom; public int PointsDeVie; } public record Stats(int Vie, int Force);

public static class Scenario
{
    public static void Executer()
    {
        var e1 = new PointS { X = 5 };
        var e2 = e1;
        e2.X++;

        var j1 = new Joueur { Nom = "Lancelot", PointsDeVie = 100 };
        var j2 = j1;
        j2.PointsDeVie -= 30;

        var tableau = new[] { e1, e2 };
        tableau[0].X = 42;

        Console.WriteLine($"{e1.X} {e2.X}");
        Console.WriteLine($"{j1.PointsDeVie} {j2.PointsDeVie}");
        Console.WriteLine($"{tableau[0].X} {e1.X}");
        Console.WriteLine(new Stats(1, 2) == new Stats(1, 2));
        Console.WriteLine(ReferenceEquals(j1, j2));
    }
}
