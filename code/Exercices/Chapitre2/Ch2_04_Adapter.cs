// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie II — Patterns Structurels/4. Adapter.md

namespace CoursPOO.Exercices.Ch2_04_Adapter;

public class AncienTableauDesScores
{
    private readonly List<string> lignes = new();

    public void EnregistrerScore(string joueur, string scoreTexte)
    {
        lignes.Add(scoreTexte);
        Console.WriteLine($"[Ancien système] {joueur} : {scoreTexte}");
    }

    public string MeilleurScoreTexte() => lignes.Count == 0 ? "000000" : lignes.Max();
}

public interface IClassement
{
    void Ajouter(string joueur, int score);
    int MeilleurScore { get; }
}

public class ClassementAdapter : IClassement
{
    private readonly AncienTableauDesScores ancien;

    public ClassementAdapter(AncienTableauDesScores ancien) => this.ancien = ancien;

    public void Ajouter(string joueur, int score)
    {
        if (score < 0 || score > 999_999)
            throw new ArgumentOutOfRangeException(nameof(score), "L’ancien système accepte de 0 à 999 999.");

        ancien.EnregistrerScore(joueur, score.ToString("D6"));   // int → "001250"
    }

    public int MeilleurScore => int.Parse(ancien.MeilleurScoreTexte());   // "001250" → int
}

public static class Scenario
{
    public static void Executer()
    {
        IClassement classement = new ClassementAdapter(new AncienTableauDesScores());
        classement.Ajouter("Arthur", 1250);
        classement.Ajouter("Morgane", 980);
        classement.Ajouter("Lancelot", 15);

        Console.WriteLine($"Meilleur score : {classement.MeilleurScore}");
    }
}
