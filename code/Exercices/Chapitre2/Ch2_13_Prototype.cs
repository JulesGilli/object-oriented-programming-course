// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/13. Prototype.md

namespace CoursPOO.Exercices.Ch2_13_Prototype;

public interface IPrototype<T> { T Cloner(); }

public class Personnage : IPrototype<Personnage>
{
    public string Nom;
    public int Niveau;
    public List<string> Inventaire = new();

    public Personnage Cloner()
    {
        var copie = (Personnage)MemberwiseClone();   // copie Nom et Niveau
        copie.Inventaire = new List<string>(Inventaire);  // duplique la liste mutable
        return copie;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var heros = new Personnage { Nom = "Arthur", Niveau = 5, Inventaire = { "Épée" } };
        var clone = heros.Cloner();
        clone.Nom = "Arthur (illusion)";
        clone.Niveau = 1;
        clone.Inventaire.Add("Miroir");

        Console.WriteLine($"{heros.Nom} niv. {heros.Niveau} : {string.Join(", ", heros.Inventaire)}");
        Console.WriteLine($"{clone.Nom} niv. {clone.Niveau} : {string.Join(", ", clone.Inventaire)}");
    }
}
