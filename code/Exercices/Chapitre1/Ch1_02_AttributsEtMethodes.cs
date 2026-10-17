// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie I — Les Fondamentaux/2. Attributs et Méthodes.md

namespace CoursPOO.Exercices.Ch1_02_AttributsEtMethodes;

public class Gecko
{
    public static int NombreDeGeckos;

    private int energie = 100;
    public string Nom;

    public Gecko(string nom)
    {
        Nom = nom;
        NombreDeGeckos++;
    }

    public void Manger()
    {
        energie = Math.Min(100, energie + 10);
        Console.WriteLine($"{Nom} mange. Énergie : {energie}");
    }

    public void Travailler()
    {
        if (energie < 25)
        {
            Console.WriteLine($"{Nom} est trop fatigué pour travailler.");
            return;
        }

        energie -= 9;
        Console.WriteLine($"{Nom} travaille. Énergie : {energie}");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var kevin = new Gecko("Kevin");
        for (int i = 0; i < 10; i++)
            kevin.Travailler();

        kevin.Manger();
        kevin.Travailler();

        var lea = new Gecko("Léa");
        Console.WriteLine($"Geckos créés : {Gecko.NombreDeGeckos}");
    }
}
