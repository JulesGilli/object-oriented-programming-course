// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie III — Concepts Avancés/8. Composition vs héritage.md

namespace CoursPOO.Exercices.Ch1_08_CompositionVsHeritage;

public interface IDeplacement
{
    string SeDeplacer();
}

public class Marche : IDeplacement { public string SeDeplacer() => "marche"; }
public class Vol : IDeplacement { public string SeDeplacer() => "vole"; }
public class Nage : IDeplacement { public string SeDeplacer() => "nage"; }

public interface IArme
{
    string Frapper();
}

public class Epee : IArme { public string Frapper() => "donne un coup d’épée"; }
public class Arc : IArme { public string Frapper() => "tire une flèche"; }

public class Personnage
{
    private IDeplacement deplacement;
    private IArme arme;

    public string Nom { get; }

    public Personnage(string nom, IDeplacement deplacement, IArme arme)
    {
        Nom = nom;
        this.deplacement = deplacement;
        this.arme = arme;
    }

    public void ChangerDeplacement(IDeplacement nouveau) => deplacement = nouveau;
    public void ChangerArme(IArme nouvelle) => arme = nouvelle;

    public void Agir()
    {
        Console.WriteLine($"{Nom} {deplacement.SeDeplacer()} et {arme.Frapper()}.");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var heros = new Personnage("Arthur", new Marche(), new Epee());
        heros.Agir();

        heros.ChangerArme(new Arc());
        heros.Agir();

        heros.ChangerDeplacement(new Nage());
        heros.Agir();

        var dragon = new Personnage("Smaug", new Vol(), new Epee());
        dragon.Agir();
    }
}
