// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 2/Partie III — Patterns Comportementaux/10. Command.md

namespace CoursPOO.Exercices.Ch2_10_Command;

public interface ICommande { void Executer(); void Annuler(); }

public class Robot
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Deplacer(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

    public override string ToString() => $"({X}, {Y})";
}

public class CommandeDeplacement : ICommande
{
    private readonly Robot robot;
    private readonly int dx, dy;

    public CommandeDeplacement(Robot robot, int dx, int dy)
    {
        this.robot = robot;
        this.dx = dx;
        this.dy = dy;
    }

    public void Executer() => robot.Deplacer(dx, dy);
    public void Annuler() => robot.Deplacer(-dx, -dy);   // un déplacement est parfaitement réversible
}

public class GestionnaireCommandes
{
    private readonly Stack<ICommande> historique = new();
    private readonly Stack<ICommande> annulees = new();

    public void ExecuterCommande(ICommande commande)
    {
        commande.Executer();
        historique.Push(commande);
        annulees.Clear();
    }

    public void Annuler()
    {
        if (historique.Count == 0) return;
        var commande = historique.Pop();
        commande.Annuler();
        annulees.Push(commande);
    }

    public void Refaire()
    {
        if (annulees.Count == 0) return;
        var commande = annulees.Pop();
        commande.Executer();
        historique.Push(commande);
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var robot = new Robot();
        var gestionnaire = new GestionnaireCommandes();

        gestionnaire.ExecuterCommande(new CommandeDeplacement(robot, 1, 0));
        gestionnaire.ExecuterCommande(new CommandeDeplacement(robot, 0, 2));
        gestionnaire.ExecuterCommande(new CommandeDeplacement(robot, 3, 0));
        Console.WriteLine($"Après 3 déplacements : {robot}");

        gestionnaire.Annuler();
        gestionnaire.Annuler();
        Console.WriteLine($"Après 2 annulations : {robot}");

        gestionnaire.Refaire();
        Console.WriteLine($"Après « refaire » : {robot}");

        gestionnaire.ExecuterCommande(new CommandeDeplacement(robot, 0, -5));
        gestionnaire.Refaire();   // sans effet : la nouvelle commande a vidé la pile « refaire »
        Console.WriteLine($"Après un nouveau déplacement : {robot}");
    }
}
