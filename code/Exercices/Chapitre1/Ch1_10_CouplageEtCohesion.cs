// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie III — Concepts Avancés/10. Couplage et Cohésion.md

namespace CoursPOO.Exercices.Ch1_10_CouplageEtCohesion;

public interface ISortie
{
    void Ecrire(string ligne);
}

public class SortieConsole : ISortie
{
    public void Ecrire(string ligne) => Console.WriteLine(ligne);
}

public class CalculateurScore
{
    public int Calculer(int ennemisVaincus, int pieces, int morts)
        => ennemisVaincus * 100 + pieces * 10 - morts * 50;

    public string Rang(int score) => score switch
    {
        >= 1000 => "Or",
        >= 500  => "Argent",
        _       => "Bronze"
    };
}

public class FormateurRapport
{
    public List<string> Formater(int score, string rang) => new()
    {
        "=== Rapport de partie ===",
        $"Score : {score}",
        $"Rang : {rang}"
    };
}

public class RapportDePartie
{
    private readonly CalculateurScore calculateur;
    private readonly FormateurRapport formateur;
    private readonly ISortie sortie;

    public RapportDePartie(CalculateurScore calculateur, FormateurRapport formateur, ISortie sortie)
    {
        this.calculateur = calculateur;
        this.formateur = formateur;
        this.sortie = sortie;
    }

    public void Generer(int ennemisVaincus, int pieces, int morts)
    {
        int score = calculateur.Calculer(ennemisVaincus, pieces, morts);
        foreach (string ligne in formateur.Formater(score, calculateur.Rang(score)))
            sortie.Ecrire(ligne);
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var rapport = new RapportDePartie(new CalculateurScore(), new FormateurRapport(), new SortieConsole());
        rapport.Generer(ennemisVaincus: 12, pieces: 15, morts: 1);

        var calculateur = new CalculateurScore();
        Console.WriteLine(calculateur.Rang(calculateur.Calculer(3, 20, 0)));
    }
}
