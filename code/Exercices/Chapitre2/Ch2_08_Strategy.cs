// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie III — Patterns Comportementaux/8. Strategy.md

namespace CoursPOO.Exercices.Ch2_08_Strategy;

public class Ennemi
{
    public string Nom { get; init; }
    public int Distance { get; init; }
    public int PointsDeVie { get; init; }
}

public interface IStrategieCiblage
{
    Ennemi Choisir(IReadOnlyList<Ennemi> ennemis);
}

public class PlusProche : IStrategieCiblage
{
    public Ennemi Choisir(IReadOnlyList<Ennemi> ennemis) => ennemis.OrderBy(e => e.Distance).First();
}

public class PlusFaible : IStrategieCiblage
{
    public Ennemi Choisir(IReadOnlyList<Ennemi> ennemis) => ennemis.OrderBy(e => e.PointsDeVie).First();
}

public class PlusResistant : IStrategieCiblage
{
    public Ennemi Choisir(IReadOnlyList<Ennemi> ennemis) => ennemis.OrderByDescending(e => e.PointsDeVie).First();
}

public class Tourelle
{
    private IStrategieCiblage strategie;

    public Tourelle(IStrategieCiblage strategie) => this.strategie = strategie;

    public void ChangerStrategie(IStrategieCiblage nouvelle) => strategie = nouvelle;

    public void Tirer(IReadOnlyList<Ennemi> ennemis)
    {
        Ennemi cible = strategie.Choisir(ennemis);
        Console.WriteLine($"La tourelle vise {cible.Nom} ({strategie.GetType().Name}).");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var ennemis = new List<Ennemi>
        {
            new() { Nom = "Gobelin", Distance = 12, PointsDeVie = 30 },
            new() { Nom = "Ogre",    Distance = 25, PointsDeVie = 200 },
            new() { Nom = "Rat",     Distance = 15, PointsDeVie = 10 },
            new() { Nom = "Orc",     Distance = 8,  PointsDeVie = 80 }
        };

        var tourelle = new Tourelle(new PlusProche());
        tourelle.Tirer(ennemis);

        tourelle.ChangerStrategie(new PlusResistant());
        tourelle.Tirer(ennemis);

        tourelle.ChangerStrategie(new PlusFaible());
        tourelle.Tirer(ennemis);
    }
}
