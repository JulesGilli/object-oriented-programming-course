// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie II — Les 4 piliers de la POO/5. Abstraction.md

namespace CoursPOO.Exercices.Ch1_05_Abstraction;

public interface IRechargeable
{
    void Recharger();
}

public abstract class Arme
{
    public string Nom { get; }
    public int Degats { get; }
    public int CoutEnergie { get; }

    protected Arme(string nom, int degats, int coutEnergie)
    {
        Nom = nom;
        Degats = degats;
        CoutEnergie = coutEnergie;
    }

    protected abstract string Bruit();

    public void Attaquer()
    {
        Console.WriteLine($"{Nom} : {Bruit()} ({Degats} dégâts, {CoutEnergie} énergie)");
    }
}

public class FusilPlasma : Arme, IRechargeable
{
    private int cellules = 3;

    public FusilPlasma() : base("Fusil à plasma", 21, 5) { }

    protected override string Bruit() => "Bzziou !";

    public void Recharger()
    {
        cellules = 3;
        Console.WriteLine($"{Nom} rechargé : {cellules} cellules.");
    }
}

public class GantEnergetique : Arme
{
    public GantEnergetique() : base("Gant énergétique", 50, 10) { }

    protected override string Bruit() => "Vrrr… BAM !";
}

public static class Scenario
{
    public static void Executer()
    {
        var armes = new List<Arme> { new FusilPlasma(), new GantEnergetique() };

        foreach (var arme in armes)
            arme.Attaquer();

        IRechargeable rechargeable = new FusilPlasma();
        rechargeable.Recharger();
    }
}
