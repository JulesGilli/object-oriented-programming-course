// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Projet — Boulangerie.md

namespace CoursPOO.Exercices.Ch2_Projet_Boulangerie;

// ---------- Outils
public static class Prix
{
    // Les prix sont stockés en centimes (int) : pas d’arrondi, pas de souci de virgule
    public static string Formater(int centimes) => $"{centimes / 100},{centimes % 100:00} €";
}

public class ProduitInconnuException : Exception
{
    public ProduitInconnuException(string nom) : base($"Produit inconnu : « {nom} ».") { }
}

public class RuptureDeStockException : Exception
{
    public string Article { get; }

    public RuptureDeStockException(string article) : base($"Rupture de stock : {article}.")
        => Article = article;
}

// ---------- Les produits
public interface IProduit
{
    string Nom { get; }
    int PrixCentimes { get; }
    int Calories { get; }
    IEnumerable<string> ArticlesAPrelever();   // ce qu’il faut sortir du stock
}

public abstract class ProduitSimple : IProduit
{
    public string Nom { get; }
    public int PrixCentimes { get; }
    public int Calories { get; }

    protected ProduitSimple(string nom, int prixCentimes, int calories)
    {
        Nom = nom;
        PrixCentimes = prixCentimes;
        Calories = calories;
    }

    public IEnumerable<string> ArticlesAPrelever()
    {
        yield return Nom;
    }
}

public abstract class Viennoiserie : ProduitSimple
{
    protected Viennoiserie(string nom, int prix, int calories) : base(nom, prix, calories) { }
}

public abstract class Sandwich : ProduitSimple
{
    protected Sandwich(string nom, int prix, int calories) : base(nom, prix, calories) { }
}

public class Croissant : Viennoiserie { public Croissant() : base("Croissant", 120, 230) { } }
public class PainAuChocolat : Viennoiserie { public PainAuChocolat() : base("Pain au chocolat", 140, 280) { } }
public class JambonBeurre : Sandwich { public JambonBeurre() : base("Jambon-beurre", 450, 420) { } }

// Les boissons sont décorables : leurs propriétés sont abstraites
public abstract class Boisson : IProduit
{
    public abstract string Nom { get; }
    public abstract int PrixCentimes { get; }
    public abstract int Calories { get; }
    public abstract IEnumerable<string> ArticlesAPrelever();
}

public class Cafe : Boisson
{
    public override string Nom => "Café";
    public override int PrixCentimes => 150;
    public override int Calories => 2;
    public override IEnumerable<string> ArticlesAPrelever() { yield return "Café"; }
}

public class JusDOrange : Boisson
{
    public override string Nom => "Jus d’orange";
    public override int PrixCentimes => 300;
    public override int Calories => 110;
    public override IEnumerable<string> ArticlesAPrelever() { yield return "Jus d’orange"; }
}

// ---------- Decorator : options de boisson
public abstract class OptionBoisson : Boisson
{
    protected readonly Boisson boisson;
    protected OptionBoisson(Boisson boisson) => this.boisson = boisson;
}

public class GrandFormat : OptionBoisson
{
    public GrandFormat(Boisson boisson) : base(boisson) { }
    public override string Nom => boisson.Nom + " grand format";
    public override int PrixCentimes => boisson.PrixCentimes + 50;
    public override int Calories => boisson.Calories * 2;
    public override IEnumerable<string> ArticlesAPrelever() => boisson.ArticlesAPrelever();
}

public class Sirop : OptionBoisson
{
    public Sirop(Boisson boisson) : base(boisson) { }
    public override string Nom => boisson.Nom + " + sirop";
    public override int PrixCentimes => boisson.PrixCentimes + 30;
    public override int Calories => boisson.Calories + 50;
    public override IEnumerable<string> ArticlesAPrelever() => boisson.ArticlesAPrelever().Append("Sirop");
}

// ---------- Génériques + Composite : les menus
public abstract class Menu<TBoisson, TPlat> : IProduit
    where TBoisson : Boisson
    where TPlat : IProduit
{
    public TBoisson Boisson { get; }
    public TPlat Plat { get; }

    protected Menu(TBoisson boisson, TPlat plat)
    {
        Boisson = boisson;
        Plat = plat;
    }

    protected abstract string Formule { get; }

    public string Nom => $"{Formule} ({Boisson.Nom}, {Plat.Nom})";
    public int PrixCentimes => (Boisson.PrixCentimes + Plat.PrixCentimes) * 90 / 100;   // -10 %
    public int Calories => Boisson.Calories + Plat.Calories;
    public IEnumerable<string> ArticlesAPrelever() => Boisson.ArticlesAPrelever().Concat(Plat.ArticlesAPrelever());
}

public class PetitDejeuner : Menu<Boisson, Viennoiserie>
{
    public PetitDejeuner(Boisson boisson, Viennoiserie viennoiserie) : base(boisson, viennoiserie) { }
    protected override string Formule => "Petit-déjeuner";
}

public class Dejeuner : Menu<Boisson, Sandwich>
{
    public Dejeuner(Boisson boisson, Sandwich sandwich) : base(boisson, sandwich) { }
    protected override string Formule => "Déjeuner";
}

// ---------- Simple Factory : le catalogue
public static class Catalogue
{
    public static IProduit Creer(string nom) => nom switch
    {
        "Croissant"        => new Croissant(),
        "Pain au chocolat" => new PainAuChocolat(),
        "Jambon-beurre"    => new JambonBeurre(),
        "Café"             => new Cafe(),
        "Jus d’orange"     => new JusDOrange(),
        _ => throw new ProduitInconnuException(nom)
    };
}

// ---------- Observer : le stock prévient quand il devient bas
public class Stock
{
    private readonly Dictionary<string, int> quantites = new();
    private readonly int seuilAlerte;

    public event Action<string, int> StockBas;

    public Stock(int seuilAlerte) => this.seuilAlerte = seuilAlerte;

    public void Approvisionner(string article, int quantite)
    {
        if (quantite <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantite), "La quantité doit être positive.");
        quantites[article] = Quantite(article) + quantite;
    }

    public int Quantite(string article) => quantites.GetValueOrDefault(article);

    // Tout ou rien : on vérifie TOUS les articles avant d’en retirer un seul
    public void Prelever(IEnumerable<string> articles)
    {
        var besoins = articles.GroupBy(a => a).ToDictionary(g => g.Key, g => g.Count());

        foreach (var (article, nombre) in besoins)
            if (Quantite(article) < nombre)
                throw new RuptureDeStockException(article);

        foreach (var (article, nombre) in besoins)
        {
            quantites[article] -= nombre;
            if (quantites[article] <= seuilAlerte)
                StockBas?.Invoke(article, quantites[article]);
        }
    }
}

// ---------- Strategy : les remises
public interface IRemise
{
    string Description { get; }
    int Appliquer(int totalCentimes);
}

public class SansRemise : IRemise
{
    public string Description => "Aucune remise";
    public int Appliquer(int totalCentimes) => totalCentimes;
}

public class RemiseEtudiant : IRemise
{
    public string Description => "Remise étudiant (-15 %)";
    public int Appliquer(int totalCentimes) => totalCentimes * 85 / 100;
}

// ---------- La commande
public class Commande
{
    private readonly List<IProduit> lignes = new();
    private readonly Stock stock;
    private IRemise remise;

    public Commande(Stock stock, IRemise remise)
    {
        this.stock = stock;
        this.remise = remise;
    }

    public void ChangerRemise(IRemise nouvelle) => remise = nouvelle;

    public void Ajouter(IProduit produit)
    {
        stock.Prelever(produit.ArticlesAPrelever());   // peut lever RuptureDeStockException
        lignes.Add(produit);
    }

    public int Total => remise.Appliquer(lignes.Sum(p => p.PrixCentimes));

    public void AfficherTicket()
    {
        Console.WriteLine("===== Ticket =====");
        foreach (var produit in lignes)
            Console.WriteLine($"{produit.Nom} : {Prix.Formater(produit.PrixCentimes)}");
        Console.WriteLine(remise.Description);
        Console.WriteLine($"Total : {Prix.Formater(Total)} ({lignes.Sum(p => p.Calories)} kcal)");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var stock = new Stock(seuilAlerte: 1);
        stock.Approvisionner("Café", 10);
        stock.Approvisionner("Croissant", 3);
        stock.Approvisionner("Sirop", 1);
        stock.Approvisionner("Jus d’orange", 4);
        stock.Approvisionner("Jambon-beurre", 2);

        stock.StockBas += (article, reste) => Console.WriteLine($"[Alerte stock] Plus que {reste} × {article}");

        var commande = new Commande(stock, new SansRemise());

        commande.Ajouter(new PetitDejeuner(new Sirop(new GrandFormat(new Cafe())), new Croissant()));
        commande.Ajouter(Catalogue.Creer("Croissant"));
        commande.Ajouter(new Dejeuner(new JusDOrange(), new JambonBeurre()));

        try
        {
            commande.Ajouter(new Sirop(new Cafe()));
        }
        catch (RuptureDeStockException ex)
        {
            Console.WriteLine($"Impossible : {ex.Message}");
        }

        try
        {
            commande.Ajouter(Catalogue.Creer("Baguette"));
        }
        catch (ProduitInconnuException ex)
        {
            Console.WriteLine($"Impossible : {ex.Message}");
        }

        commande.ChangerRemise(new RemiseEtudiant());
        commande.AfficherTicket();
        Console.WriteLine($"Cafés restants : {stock.Quantite("Café")}");
    }
}
