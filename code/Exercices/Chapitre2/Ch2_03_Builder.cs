// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie I — Patterns Créationnels/3. Builder.md

namespace CoursPOO.Exercices.Ch2_03_Builder;

public class Quete
{
    public string Titre { get; }
    public IReadOnlyList<string> Objectifs { get; }
    public int Recompense { get; }

    internal Quete(string titre, List<string> objectifs, int recompense)
    {
        Titre = titre;
        Objectifs = objectifs;
        Recompense = recompense;
    }

    public void Decrire()
    {
        Console.WriteLine($"Quête : {Titre} ({Recompense} pièces d’or)");
        for (int i = 0; i < Objectifs.Count; i++)
            Console.WriteLine($"  {i + 1}. {Objectifs[i]}");
    }
}

public class QueteBuilder
{
    private string titre;
    private readonly List<string> objectifs = new();
    private int recompense;

    public QueteBuilder AvecTitre(string titre)
    {
        this.titre = titre;
        return this;
    }

    public QueteBuilder AjouterObjectif(string objectif)
    {
        objectifs.Add(objectif);
        return this;
    }

    public QueteBuilder AvecRecompense(int pieces)
    {
        recompense = pieces;
        return this;
    }

    public Quete Construire()
    {
        if (string.IsNullOrWhiteSpace(titre))
            throw new InvalidOperationException("Une quête doit avoir un titre.");
        if (objectifs.Count == 0)
            throw new InvalidOperationException("Une quête doit avoir au moins un objectif.");
        if (recompense < 0)
            throw new InvalidOperationException("La récompense ne peut pas être négative.");

        return new Quete(titre, new List<string>(objectifs), recompense);   // copie : le builder reste réutilisable
    }
}

public static class Scenario
{
    public static void Executer()
    {
        Quete quete = new QueteBuilder()
            .AvecTitre("Le dragon du nord")
            .AjouterObjectif("Trouver la caverne")
            .AjouterObjectif("Vaincre le dragon")
            .AvecRecompense(500)
            .Construire();
        quete.Decrire();

        try
        {
            new QueteBuilder().AvecTitre("Quête vide").Construire();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }
    }
}
