// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie I — Les Fondamentaux/1. Classe et Objet.md

namespace CoursPOO.Exercices.Ch1_01_ClasseEtObjet;

public class Gecko
{
    public string Nom;
    public int Age;

    public void SePresenter()
    {
        Console.WriteLine($"Bonjour, je suis {Nom} et j’ai {Age} ans.");
    }

    public void Vieillir()
    {
        Age++;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var kevin = new Gecko();
        kevin.Nom = "Kevin";
        kevin.Age = 2;

        var lea = new Gecko();
        lea.Nom = "Léa";
        lea.Age = 5;

        kevin.SePresenter();
        lea.SePresenter();

        kevin.Vieillir();
        kevin.SePresenter();
        lea.SePresenter();
    }
}
