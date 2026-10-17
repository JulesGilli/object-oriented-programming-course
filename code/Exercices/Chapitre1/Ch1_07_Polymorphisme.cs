// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie II — Les 4 piliers de la POO/7. Polymorphisme.md

namespace CoursPOO.Exercices.Ch1_07_Polymorphisme;

public abstract class Combattant
{
    public string Nom { get; }

    protected Combattant(string nom) => Nom = nom;

    public abstract int Attaque();
    public virtual string Cri() => "…";
}

public class Guerrier : Combattant
{
    public Guerrier(string nom) : base(nom) { }
    public override int Attaque() => 12;
    public override string Cri() => "Pour l’honneur !";
}

public class Mage : Combattant
{
    public Mage(string nom) : base(nom) { }
    public override int Attaque() => 15;
    public override string Cri() => "Abracadabra !";
}

public class Archer : Combattant
{
    public Archer(string nom) : base(nom) { }
    public override int Attaque() => 10;   // garde le cri par défaut
}

public static class Scenario
{
    public static void Executer()
    {
        var equipe = new List<Combattant>
        {
            new Guerrier("Conan"),
            new Mage("Merlin"),
            new Archer("Robin")
        };

        int total = 0;
        foreach (var c in equipe)
        {
            Console.WriteLine($"{c.Nom} crie « {c.Cri()} » et inflige {c.Attaque()} dégâts.");
            total += c.Attaque();
        }

        Console.WriteLine($"Dégâts totaux : {total}");
    }
}
