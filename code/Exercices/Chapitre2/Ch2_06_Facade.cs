// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie II — Patterns Structurels/6. Facade.md

namespace CoursPOO.Exercices.Ch2_06_Facade;

public interface IAudio { void JouerMusique(string piste); }
public interface IChargeurNiveau { void Charger(int niveau); }
public interface ISauvegarde
{
    int DernierNiveau();
    void Sauvegarder(int niveau);
}

public class JeuFacade
{
    private readonly IAudio audio;
    private readonly IChargeurNiveau chargeur;
    private readonly ISauvegarde sauvegarde;

    public JeuFacade(IAudio audio, IChargeurNiveau chargeur, ISauvegarde sauvegarde)
    {
        this.audio = audio;
        this.chargeur = chargeur;
        this.sauvegarde = sauvegarde;
    }

    public void NouvellePartie()
    {
        sauvegarde.Sauvegarder(1);
        chargeur.Charger(1);
        audio.JouerMusique("Thème principal");
    }

    public void Continuer()
    {
        int niveau = sauvegarde.DernierNiveau();
        chargeur.Charger(niveau);
        audio.JouerMusique($"Thème du niveau {niveau}");
    }
}

// Faux sous-systèmes pour le test : ils notent ce qu’on leur demande
public class Journal
{
    public List<string> Lignes { get; } = new();
}

public class FauxAudio : IAudio
{
    private readonly Journal journal;
    public FauxAudio(Journal journal) => this.journal = journal;
    public void JouerMusique(string piste) => journal.Lignes.Add($"Audio : {piste}");
}

public class FauxChargeur : IChargeurNiveau
{
    private readonly Journal journal;
    public FauxChargeur(Journal journal) => this.journal = journal;
    public void Charger(int niveau) => journal.Lignes.Add($"Chargement du niveau {niveau}");
}

public class FausseSauvegarde : ISauvegarde
{
    private readonly Journal journal;
    private int niveau;

    public FausseSauvegarde(Journal journal, int niveauSauvegarde)
    {
        this.journal = journal;
        niveau = niveauSauvegarde;
    }

    public int DernierNiveau() => niveau;

    public void Sauvegarder(int niveau)
    {
        this.niveau = niveau;
        journal.Lignes.Add($"Sauvegarde : niveau {niveau}");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var journal = new Journal();
        var jeu = new JeuFacade(new FauxAudio(journal), new FauxChargeur(journal), new FausseSauvegarde(journal, 4));

        jeu.Continuer();
        jeu.NouvellePartie();

        foreach (string ligne in journal.Lignes)
            Console.WriteLine(ligne);
    }
}
