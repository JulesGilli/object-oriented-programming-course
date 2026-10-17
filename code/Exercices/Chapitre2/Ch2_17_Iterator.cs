// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/17. Iterator.md

using System.Collections;   // pour l’interface non générique IEnumerable

namespace CoursPOO.Exercices.Ch2_17_Iterator;

public class Heros
{
    public string Nom { get; }
    public int Pv { get; set; }
    public Heros(string nom, int pv) { Nom = nom; Pv = pv; }
}

public class Equipe : IEnumerable<Heros>
{
    private readonly List<Heros> membres = new();

    public void Ajouter(Heros heros) => membres.Add(heros);

    public IEnumerator<Heros> GetEnumerator() => membres.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<Heros> Vivants()
    {
        foreach (var heros in membres)
            if (heros.Pv > 0)
                yield return heros;
    }

    public IEnumerable<Heros> TourDeJeu()
    {
        while (true)
        {
            bool quelquUnAJoue = false;
            foreach (var heros in Vivants())
            {
                quelquUnAJoue = true;
                yield return heros;
            }
            if (!quelquUnAJoue)
                yield break;    // tout le monde est KO : la séquence s’arrête
        }
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var equipe = new Equipe();
        equipe.Ajouter(new Heros("Ayla", 30));
        equipe.Ajouter(new Heros("Bran", 0));
        equipe.Ajouter(new Heros("Cyd", 12));

        Console.WriteLine(string.Join(", ", equipe.Select(h => h.Nom)));
        Console.WriteLine(string.Join(", ", equipe.Vivants().Select(h => h.Nom)));
        Console.WriteLine(string.Join(" → ", equipe.TourDeJeu().Take(5).Select(h => h.Nom)));

        foreach (var heros in equipe) heros.Pv = 0;
        Console.WriteLine($"Tours possibles quand tout le monde est KO : {equipe.TourDeJeu().Count()}");
    }
}
