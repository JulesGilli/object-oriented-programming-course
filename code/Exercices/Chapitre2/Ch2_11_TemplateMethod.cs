// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/11. Template Method.md

namespace CoursPOO.Exercices.Ch2_11_TemplateMethod;

public abstract class Recette
{
    public void Preparer()
    {
        Console.WriteLine($"== {Nom} ==");
        Petrir();
        if (DoitReposer())
            Console.WriteLine("Repos de la pâte : 1 h.");
        Cuire();
        Console.WriteLine("Prêt !");
    }

    protected abstract string Nom { get; }
    protected virtual void Petrir() => Console.WriteLine("Pétrir la pâte.");
    protected virtual bool DoitReposer() => true;   // hook
    protected abstract void Cuire();
}

public class Baguette : Recette
{
    protected override string Nom => "Baguette";
    protected override void Cuire() => Console.WriteLine("Cuire 25 min à 240 °C.");
}

public class Cookie : Recette
{
    protected override string Nom => "Cookie";
    protected override void Petrir() => Console.WriteLine("Mélanger beurre, sucre et pépites.");
    protected override bool DoitReposer() => false;
    protected override void Cuire() => Console.WriteLine("Cuire 10 min à 180 °C.");
}

public static class Scenario
{
    public static void Executer()
    {
        new Baguette().Preparer();
        new Cookie().Preparer();
    }
}
