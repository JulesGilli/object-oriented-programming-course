// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie IV — Patterns complémentaires/12. Composite.md

namespace CoursPOO.Exercices.Ch2_12_Composite;

public interface IPhrase
{
    string Texte();
    int NombreDeMots();
}

public class Mot : IPhrase
{
    private readonly string mot;
    public Mot(string mot) => this.mot = mot;
    public string Texte() => mot;
    public int NombreDeMots() => 1;
}

public class PhraseComposite : IPhrase
{
    private readonly List<IPhrase> parties = new();

    public PhraseComposite Ajouter(IPhrase partie)
    {
        parties.Add(partie);
        return this;
    }

    public string Texte() => string.Join(" ", parties.Select(p => p.Texte()));
    public int NombreDeMots() => parties.Sum(p => p.NombreDeMots());
}

public static class Scenario
{
    public static void Executer()
    {
        var debut = new PhraseComposite().Ajouter(new Mot("Le")).Ajouter(new Mot("dragon"));
        var fin = new PhraseComposite().Ajouter(new Mot("crache")).Ajouter(new Mot("du")).Ajouter(new Mot("feu"));
        var phrase = new PhraseComposite().Ajouter(debut).Ajouter(fin);

        Console.WriteLine(phrase.Texte());
        Console.WriteLine($"{phrase.NombreDeMots()} mots");
    }
}
