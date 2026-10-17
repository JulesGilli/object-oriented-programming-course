// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie IV — Les outils du langage/13. Membres statiques, énumérations et namespaces.md

namespace CoursPOO.Exercices.Ch1_13_MembresStatiquesEnumerationsEtNamespaces;

public enum TypeEnnemi { Gobelin, Orc, Dragon }

public class Ennemi
{
    private static readonly Dictionary<TypeEnnemi, int> compteurs = new();

    public TypeEnnemi Type { get; }

    public Ennemi(TypeEnnemi type)
    {
        Type = type;
        compteurs[type] = NombreDe(type) + 1;
    }

    public static int NombreDe(TypeEnnemi type)
        => compteurs.TryGetValue(type, out int nombre) ? nombre : 0;

    public int PointsDeVie => Type switch
    {
        TypeEnnemi.Gobelin => 30,
        TypeEnnemi.Orc     => 80,
        TypeEnnemi.Dragon  => 500,
        _ => throw new ArgumentOutOfRangeException()
    };
}

public static class Scenario
{
    public static void Executer()
    {
        var ennemis = new List<Ennemi>
        {
            new(TypeEnnemi.Gobelin), new(TypeEnnemi.Gobelin),
            new(TypeEnnemi.Orc), new(TypeEnnemi.Gobelin)
        };

        foreach (TypeEnnemi type in Enum.GetValues<TypeEnnemi>())
            Console.WriteLine($"{type} : {Ennemi.NombreDe(type)}");

        Console.WriteLine($"PV totaux : {ennemis.Sum(e => e.PointsDeVie)}");
    }
}
