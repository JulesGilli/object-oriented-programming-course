// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie IV — Les outils du langage/16. Délégués, événements et lambdas.md

namespace CoursPOO.Exercices.Ch1_16_DeleguesEvenementsEtLambdas;

public class Minuteur
{
    private int secondesRestantes;

    public event Action<int> OnSecondeEcoulee;
    public event Action OnTermine;

    public Minuteur(int secondes) => secondesRestantes = secondes;

    public void AvancerUneSeconde()
    {
        if (secondesRestantes == 0)
            return;

        secondesRestantes--;
        OnSecondeEcoulee?.Invoke(secondesRestantes);

        if (secondesRestantes == 0)
            OnTermine?.Invoke();
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var minuteur = new Minuteur(3);
        Action<int> afficher = s => Console.WriteLine($"Il reste {s} s");
        minuteur.OnSecondeEcoulee += afficher;
        minuteur.OnTermine += () => Console.WriteLine("Boum !");

        minuteur.AvancerUneSeconde();
        minuteur.OnSecondeEcoulee -= afficher;      // fonctionne : même instance de délégué
        minuteur.AvancerUneSeconde();
        minuteur.AvancerUneSeconde();
        minuteur.AvancerUneSeconde();     // déjà terminé : rien ne se passe
    }
}
