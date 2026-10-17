// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie I — Patterns Créationnels/2. Factory Method.md

namespace CoursPOO.Exercices.Ch2_02_FactoryMethod;

public abstract class Jouet
{
    public abstract string Nom { get; }
    public abstract string Jouer();
}

public class Peluche : Jouet
{
    public override string Nom => "Ours en peluche";
    public override string Jouer() => "Câlin !";
}

public class ConsolePortable : Jouet
{
    public override string Nom => "Console portable";
    public override string Jouer() => "Bip bip !";
}

public abstract class Atelier
{
    // Logique commune : fabriquer, contrôler, emballer
    public Jouet Livrer()
    {
        Jouet jouet = FabriquerJouet();
        Console.WriteLine($"Contrôle qualité : {jouet.Nom}… OK");
        Console.WriteLine($"Emballage : {jouet.Nom}");
        return jouet;
    }

    protected abstract Jouet FabriquerJouet();   // la factory method
}

public class AtelierPeluches : Atelier
{
    protected override Jouet FabriquerJouet() => new Peluche();
}

public class AtelierConsoles : Atelier
{
    protected override Jouet FabriquerJouet() => new ConsolePortable();
}

public static class Scenario
{
    public static void Executer()
    {
        var ateliers = new List<Atelier> { new AtelierPeluches(), new AtelierConsoles() };

        foreach (var atelier in ateliers)
        {
            Jouet cadeau = atelier.Livrer();
            Console.WriteLine($"L’enfant joue : {cadeau.Jouer()}");
        }
    }
}
