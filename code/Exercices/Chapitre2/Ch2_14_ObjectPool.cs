// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/14. Object Pool.md

namespace CoursPOO.Exercices.Ch2_14_ObjectPool;

public class Projectile { private static int compteur; public int Id { get; } = ++compteur; public int DistanceParcourue { get; set; } }

public class PoolBorne<T> where T : class
{
    private readonly Stack<T> disponibles = new();
    private readonly Func<T> creer;
    private readonly int tailleMax;

    public int NombreDetruits { get; private set; }
    public int NombreDisponibles => disponibles.Count;

    public PoolBorne(Func<T> creer, int tailleMax)
    {
        this.creer = creer;
        this.tailleMax = tailleMax;
    }

    public T Prendre() => disponibles.Count > 0 ? disponibles.Pop() : creer();

    public void Rendre(T objet)
    {
        if (disponibles.Contains(objet))
            throw new InvalidOperationException("Cet objet a déjà été rendu au pool.");

        if (disponibles.Count >= tailleMax)
        {
            NombreDetruits++;   // pool plein : on laisse l’objet au ramasse-miettes
            return;
        }

        disponibles.Push(objet);
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var borne = new PoolBorne<Projectile>(() => new Projectile(), tailleMax: 2);
        var a = borne.Prendre();
        var b = borne.Prendre();
        var c = borne.Prendre();
        borne.Rendre(a);
        borne.Rendre(b);
        borne.Rendre(c);
        Console.WriteLine($"Disponibles : {borne.NombreDisponibles}, détruits : {borne.NombreDetruits}");

        try
        {
            borne.Rendre(a);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }
    }
}
