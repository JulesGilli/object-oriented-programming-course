// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie I — Les Fondamentaux/3. Constructeurs.md

namespace CoursPOO.Exercices.Ch1_03_Constructeurs;

public class Gecko
{
    public string Nom;
    public int Age;

    public Gecko() : this("Inconnu") { }

    public Gecko(string nom) : this(nom, 0) { }

    public Gecko(string nom, int age)
    {
        Nom = nom;
        Age = Math.Max(0, age);
        Console.WriteLine($"Bonjour {Nom} !");   // le seul endroit qui affiche ce message
    }

    public void Decrire() => Console.WriteLine($"{Nom} a {Age} ans.");
}

public static class Scenario
{
    public static void Executer()
    {
        var g1 = new Gecko();
        var g2 = new Gecko("Kevin");
        var g3 = new Gecko("Léa", 5);
        var g4 = new Gecko("Bug", -3);

        g1.Decrire();
        g2.Decrire();
        g3.Decrire();
        g4.Decrire();
    }
}
