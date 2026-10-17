// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie II — Patterns Structurels/5. Decorator.md

namespace CoursPOO.Exercices.Ch2_05_Decorator;

public interface IPotion
{
    string Nom { get; }
    int Puissance { get; }
}

public class PotionDeSoin : IPotion
{
    public string Nom => "Potion de soin";
    public int Puissance => 20;
}

public abstract class Ingredient : IPotion
{
    protected readonly IPotion potion;
    protected Ingredient(IPotion potion) => this.potion = potion;

    public abstract string Nom { get; }
    public abstract int Puissance { get; }
}

public class Concentree : Ingredient
{
    public Concentree(IPotion potion) : base(potion) { }
    public override string Nom => potion.Nom + " concentrée";
    public override int Puissance => potion.Puissance * 2;
}

public class Epicee : Ingredient
{
    public Epicee(IPotion potion) : base(potion) { }
    public override string Nom => potion.Nom + " épicée";
    public override int Puissance => potion.Puissance + 5;
}

public static class Scenario
{
    public static void Executer()
    {
        IPotion p1 = new Concentree(new Epicee(new PotionDeSoin()));
        IPotion p2 = new Epicee(new Concentree(new PotionDeSoin()));

        Console.WriteLine($"{p1.Nom} : {p1.Puissance} PV");
        Console.WriteLine($"{p2.Nom} : {p2.Puissance} PV");
    }
}
