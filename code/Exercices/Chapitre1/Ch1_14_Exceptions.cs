// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie IV — Les outils du langage/14. Exceptions.md

namespace CoursPOO.Exercices.Ch1_14_Exceptions;

public class ProduitIndisponibleException : Exception
{
    public string Produit { get; }

    public ProduitIndisponibleException(string produit)
        : base($"Plus de « {produit} » en stock.")
    {
        Produit = produit;
    }
}

public class Stock
{
    private readonly Dictionary<string, int> quantites = new();

    public void Ajouter(string produit, int quantite)
    {
        if (quantite <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantite), "La quantité doit être positive.");

        quantites[produit] = Quantite(produit) + quantite;
    }

    public int Quantite(string produit)
        => quantites.TryGetValue(produit, out int quantite) ? quantite : 0;

    public void Prendre(string produit)
    {
        if (!TryPrendre(produit))
            throw new ProduitIndisponibleException(produit);
    }

    public bool TryPrendre(string produit)
    {
        int quantite = Quantite(produit);
        if (quantite == 0)
            return false;

        quantites[produit] = quantite - 1;
        return true;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var stock = new Stock();
        stock.Ajouter("Croissant", 2);

        stock.Prendre("Croissant");
        Console.WriteLine($"Croissants restants : {stock.Quantite("Croissant")}");
        Console.WriteLine(stock.TryPrendre("Croissant"));
        Console.WriteLine(stock.TryPrendre("Croissant"));

        try
        {
            stock.Prendre("Baguette");
        }
        catch (ProduitIndisponibleException ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }

        try
        {
            stock.Ajouter("Éclair", -3);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Erreur : {ex.ParamName}");
        }
    }
}
