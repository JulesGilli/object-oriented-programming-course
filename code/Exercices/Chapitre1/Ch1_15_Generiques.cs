// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie IV — Les outils du langage/15. Génériques.md

namespace CoursPOO.Exercices.Ch1_15_Generiques;

public abstract class Combattant : IComparable<Combattant>
{
    public string Nom { get; }
    public abstract int Puissance { get; }

    protected Combattant(string nom) => Nom = nom;

    public int CompareTo(Combattant autre) => Puissance.CompareTo(autre.Puissance);

    public override string ToString() => $"{Nom} ({Puissance})";
}

public class Guerrier : Combattant
{
    private readonly int force;
    public Guerrier(string nom, int force) : base(nom) => this.force = force;
    public override int Puissance => force * 2;
}

public class Mage : Combattant
{
    private readonly int magie;
    public Mage(string nom, int magie) : base(nom) => this.magie = magie;
    public override int Puissance => magie * 3;
}

public static class Scenario
{
    public static T MeilleurDe<T>(IEnumerable<T> candidats) where T : IComparable<T>
    {
        T meilleur = default;
        bool premier = true;

        foreach (T candidat in candidats)
        {
            if (premier || candidat.CompareTo(meilleur) > 0)
            {
                meilleur = candidat;
                premier = false;
            }
        }

        if (premier)
            throw new ArgumentException("La liste de candidats est vide.", nameof(candidats));

        return meilleur;
    }

    public static void Executer()
    {
        var equipe = new List<Combattant>
        {
            new Guerrier("Conan", 20),
            new Mage("Merlin", 15),
            new Guerrier("Xena", 18)
        };

        Console.WriteLine(MeilleurDe(equipe));
        Console.WriteLine(MeilleurDe(new[] { 4, 9, 2 }));
        Console.WriteLine(MeilleurDe(new[] { "pomme", "kiwi", "banane" }));
    }
}
