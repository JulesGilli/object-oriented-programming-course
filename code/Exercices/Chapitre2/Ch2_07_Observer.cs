// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie III — Patterns Comportementaux/7. Observer.md

namespace CoursPOO.Exercices.Ch2_07_Observer;

public class Commande
{
    private string statut = "En préparation";

    public int Numero { get; }
    public event Action<Commande> StatutChange;

    public Commande(int numero) => Numero = numero;

    public string Statut
    {
        get => statut;
        set
        {
            if (value == statut)
                return;            // pas de notification si rien ne change

            statut = value;
            StatutChange?.Invoke(this);
        }
    }
}

public class Client
{
    public string Nom { get; }

    public Client(string nom) => Nom = nom;

    public void Suivre(Commande commande) => commande.StatutChange += Notifier;
    public void NePlusSuivre(Commande commande) => commande.StatutChange -= Notifier;

    private void Notifier(Commande commande)
        => Console.WriteLine($"[{Nom}] Commande n°{commande.Numero} : {commande.Statut}");
}

public static class Scenario
{
    public static void Executer()
    {
        var commande = new Commande(42);
        var alice = new Client("Alice");
        var bob = new Client("Bob");

        alice.Suivre(commande);
        bob.Suivre(commande);

        commande.Statut = "Expédiée";
        commande.Statut = "Expédiée";   // aucun changement : aucune notification

        bob.NePlusSuivre(commande);
        commande.Statut = "Livrée";
    }
}
