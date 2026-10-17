// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie III — Concepts Avancés/11. Principes SOLID.md

namespace CoursPOO.Exercices.Ch1_11_PrincipesSOLID;

public class Joueur { public string Nom { get; } public int Niveau { get; } public int Or { get; set; } public Joueur(string nom, int niveau) { Nom = nom; Niveau = niveau; } }

public interface ICatalogue
{
    bool TryObtenirPrix(string objet, out int prix);
}

public interface IJournal
{
    void Enregistrer(string message);
}

public class CatalogueFixe : ICatalogue
{
    private readonly Dictionary<string, int> prix = new()
    {
        ["Potion"] = 10,
        ["Épée"] = 50,
    };

    public bool TryObtenirPrix(string objet, out int prix) => this.prix.TryGetValue(objet, out prix);
}

public class JournalConsole : IJournal
{
    public void Enregistrer(string message) => Console.WriteLine($"[Journal] {message}");
}

public class Boutique
{
    private readonly ICatalogue catalogue;
    private readonly IJournal journal;

    public Boutique(ICatalogue catalogue, IJournal journal)
    {
        this.catalogue = catalogue;
        this.journal = journal;
    }

    public bool Acheter(Joueur joueur, string objet)
    {
        if (!catalogue.TryObtenirPrix(objet, out int prix) || joueur.Or < prix)
            return false;

        joueur.Or -= prix;
        journal.Enregistrer($"{joueur.Nom} a acheté {objet} pour {prix} pièces d’or.");
        return true;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var boutique = new Boutique(new CatalogueFixe(), new JournalConsole());
        var joueur = new Joueur("Arthur", 5) { Or = 30 };

        Console.WriteLine(boutique.Acheter(joueur, "Potion"));
        Console.WriteLine(boutique.Acheter(joueur, "Épée"));
        Console.WriteLine($"Or restant : {joueur.Or}");
    }
}
