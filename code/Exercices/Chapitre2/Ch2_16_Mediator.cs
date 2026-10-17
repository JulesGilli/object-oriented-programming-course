// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/16. Mediator.md

namespace CoursPOO.Exercices.Ch2_16_Mediator;

public class JoueurSalon
{
    private readonly Salon salon;
    public string Nom { get; }
    public bool Pret { get; private set; }

    public JoueurSalon(string nom, Salon salon)
    {
        Nom = nom;
        this.salon = salon;
        salon.Rejoindre(this);
    }

    public void EtrePret()
    {
        if (Pret) return;
        Pret = true;
        salon.SignalerPret(this);
    }
}

public class Salon
{
    private readonly List<JoueurSalon> joueurs = new();
    private readonly int places;

    public bool PartieLancee { get; private set; }

    public Salon(int places) => this.places = places;

    public void Rejoindre(JoueurSalon joueur)
    {
        joueurs.Add(joueur);
        Console.WriteLine($"{joueur.Nom} rejoint le salon ({joueurs.Count}/{places}).");
    }

    public void SignalerPret(JoueurSalon joueur)
    {
        int prets = joueurs.Count(j => j.Pret);
        Console.WriteLine($"{joueur.Nom} a cliqué sur « Prêt » ({prets}/{places}).");

        if (prets == places)
        {
            PartieLancee = true;
            Console.WriteLine("Tout le monde est prêt : la partie commence !");
        }
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var salon = new Salon(places: 3);
        var ana = new JoueurSalon("Ana", salon);
        var ben = new JoueurSalon("Ben", salon);
        var chloe = new JoueurSalon("Chloé", salon);

        ana.EtrePret();
        ana.EtrePret();          // déjà prête : rien ne se passe
        chloe.EtrePret();
        Console.WriteLine($"Partie lancée : {salon.PartieLancee}");
        ben.EtrePret();
        Console.WriteLine($"Partie lancée : {salon.PartieLancee}");
    }
}
