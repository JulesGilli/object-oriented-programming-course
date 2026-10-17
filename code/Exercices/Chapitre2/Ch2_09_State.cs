// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie III — Patterns Comportementaux/9. State.md

namespace CoursPOO.Exercices.Ch2_09_State;

public interface IEtatPorte
{
    void Ouvrir(Porte porte);
    void Fermer(Porte porte);
    void Verrouiller(Porte porte);
    void Deverrouiller(Porte porte);
}

public class Porte
{
    private IEtatPorte etat = new Fermee();

    public void ChangerEtat(IEtatPorte nouvelEtat) => etat = nouvelEtat;

    public void Ouvrir() => etat.Ouvrir(this);
    public void Fermer() => etat.Fermer(this);
    public void Verrouiller() => etat.Verrouiller(this);
    public void Deverrouiller() => etat.Deverrouiller(this);
}

public class Fermee : IEtatPorte
{
    public void Ouvrir(Porte porte)
    {
        Console.WriteLine("La porte s’ouvre.");
        porte.ChangerEtat(new Ouverte());
    }

    public void Fermer(Porte porte) => Console.WriteLine("La porte est déjà fermée.");

    public void Verrouiller(Porte porte)
    {
        Console.WriteLine("Clic ! La porte est verrouillée.");
        porte.ChangerEtat(new Verrouillee());
    }

    public void Deverrouiller(Porte porte) => Console.WriteLine("La porte n’est pas verrouillée.");
}

public class Ouverte : IEtatPorte
{
    public void Ouvrir(Porte porte) => Console.WriteLine("La porte est déjà ouverte.");

    public void Fermer(Porte porte)
    {
        Console.WriteLine("La porte se ferme.");
        porte.ChangerEtat(new Fermee());
    }

    public void Verrouiller(Porte porte) => Console.WriteLine("Impossible de verrouiller une porte ouverte.");
    public void Deverrouiller(Porte porte) => Console.WriteLine("La porte n’est pas verrouillée.");
}

public class Verrouillee : IEtatPorte
{
    public void Ouvrir(Porte porte) => Console.WriteLine("La porte est verrouillée.");
    public void Fermer(Porte porte) => Console.WriteLine("La porte est déjà fermée.");
    public void Verrouiller(Porte porte) => Console.WriteLine("La porte est déjà verrouillée.");

    public void Deverrouiller(Porte porte)
    {
        Console.WriteLine("Clac ! La porte est déverrouillée.");
        porte.ChangerEtat(new Fermee());
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var porte = new Porte();
        porte.Ouvrir();
        porte.Verrouiller();
        porte.Fermer();
        porte.Verrouiller();
        porte.Ouvrir();
        porte.Deverrouiller();
        porte.Ouvrir();
    }
}
