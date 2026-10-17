// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Projet — Arène spatiale.md

namespace CoursPOO.Exercices.Ch1_Projet_AreneSpatiale;

public enum Camp
{
    Marines,
    Monstres
}

public interface ICombattant
{
    string Nom { get; }
    bool EstVivant { get; }
    void RecevoirDegats(int degats);
}

// ---------- Les armes
public abstract class Arme
{
    public string Nom { get; }
    public int Degats { get; }
    public int CoutPA { get; }

    protected Arme(string nom, int degats, int coutPA)
    {
        Nom = nom;
        Degats = degats;
        CoutPA = coutPA;
    }

    public abstract string Bruit();
}

public class FusilPlasma : Arme
{
    public FusilPlasma() : base("son fusil à plasma", 21, 5) { }
    public override string Bruit() => "Bzziou !";
}

public class GantEnergetique : Arme
{
    public GantEnergetique() : base("son gant énergétique", 50, 10) { }
    public override string Bruit() => "Vrrr… BAM !";
}

public class Griffes : Arme
{
    public Griffes() : base("ses griffes", 14, 4) { }
    public override string Bruit() => "Shrrk !";
}

public class Massue : Arme
{
    public Massue() : base("sa massue", 30, 8) { }
    public override string Bruit() => "Bonk !";
}

public abstract class Unite : ICombattant
{
    private static readonly Dictionary<string, int> compteurs = new();

    private int pv;
    private int pa;

    public string Nom { get; }
    public int PvMax { get; }
    public int PaMax { get; }
    public int Pv => pv;
    public int Pa => pa;
    public Arme Arme { get; private set; }
    public bool EstVivant => pv > 0;

    protected int Armure { get; }
    protected abstract int Recuperation { get; }
    public abstract Camp Camp { get; }

    public event Action<Unite> Elimine;

    protected Unite(string type, int pvMax, int paMax, int armure)
    {
        compteurs[type] = compteurs.GetValueOrDefault(type) + 1;
        Nom = $"{type} #{compteurs[type]}";
        PvMax = pvMax;
        PaMax = paMax;
        Armure = armure;
        pv = pvMax;
        pa = paMax;
    }

    public void Equiper(Arme arme)
    {
        Arme = arme ?? throw new ArgumentNullException(nameof(arme));
    }

    public void RecevoirDegats(int degats)
    {
        if (degats < 0)
            throw new ArgumentOutOfRangeException(nameof(degats), "Les dégâts ne peuvent pas être négatifs.");
        if (!EstVivant)
            return;

        int subis = Math.Max(1, degats - Armure);     // l’armure absorbe une partie, minimum 1
        pv = Math.Max(0, pv - subis);
        Console.WriteLine($"  {Nom} perd {subis} PV ({pv}/{PvMax})");

        if (!EstVivant)
        {
            Console.WriteLine($"  {Nom} est éliminé !");
            Elimine?.Invoke(this);
        }
    }

    public bool Attaquer(Unite cible)
    {
        if (cible == this)
            throw new InvalidOperationException($"{Nom} ne peut pas s’attaquer lui-même.");

        if (Arme == null)
        {
            Console.WriteLine($"{Nom} n’a pas d’arme.");
            return false;
        }

        if (pa < Arme.CoutPA)
        {
            Console.WriteLine($"{Nom} est à bout de souffle ({pa} PA).");
            return false;
        }

        pa -= Arme.CoutPA;
        Console.WriteLine($"{Nom} attaque {cible.Nom} avec {Arme.Nom} : {Arme.Bruit()}");
        cible.RecevoirDegats(Arme.Degats);
        return true;
    }

    public void RecupererPA() => pa = Math.Min(PaMax, pa + Recuperation);
}

public abstract class Marine : Unite
{
    protected Marine(string type, int pvMax, int paMax, int armure) : base(type, pvMax, paMax, armure) { }
    public override Camp Camp => Camp.Marines;
}

public abstract class Monstre : Unite
{
    protected Monstre(string type, int pvMax, int paMax, int armure) : base(type, pvMax, paMax, armure) { }
    public override Camp Camp => Camp.Monstres;
}

public class MarineTactique : Marine
{
    public MarineTactique() : base("Marine", 100, 20, 3) => Equiper(new FusilPlasma());
    protected override int Recuperation => 9;
}

public class Terminator : Marine
{
    public Terminator() : base("Terminator", 150, 30, 6) => Equiper(new GantEnergetique());
    protected override int Recuperation => 6;
}

public class Scorpion : Monstre
{
    public Scorpion() : base("Scorpion", 80, 20, 0) => Equiper(new Griffes());
    protected override int Recuperation => 6;
}

public class SuperMutant : Monstre
{
    public SuperMutant() : base("Super-mutant", 170, 20, 2) => Equiper(new Massue());
    protected override int Recuperation => 4;
}

public class Arene
{
    private readonly List<Unite> marines = new();
    private readonly List<Unite> monstres = new();

    public void Inscrire(Unite unite)
    {
        if (!unite.EstVivant)
            throw new ArgumentException($"{unite.Nom} est déjà hors de combat.", nameof(unite));

        Equipe(unite.Camp).Add(unite);
        unite.Elimine += Retirer;
        Console.WriteLine($"{unite.Nom} entre dans l’arène.");
    }

    private List<Unite> Equipe(Camp camp) => camp == Camp.Marines ? marines : monstres;

    private void Retirer(Unite unite)
    {
        unite.Elimine -= Retirer;
        Equipe(unite.Camp).Remove(unite);
    }

    private bool CombatTermine => marines.Count == 0 || monstres.Count == 0;

    public Camp? Combattre(int toursMax = 20)
    {
        for (int tour = 1; tour <= toursMax && !CombatTermine; tour++)
        {
            Console.WriteLine($"--- Tour {tour} ---");

            marines[0].Attaquer(monstres[0]);       // les marines frappent en premier
            if (CombatTermine) break;

            monstres[0].Attaquer(marines[0]);

            foreach (var unite in marines.Concat(monstres))
                unite.RecupererPA();
        }

        Camp? vainqueur = marines.Count == 0 ? Camp.Monstres
                        : monstres.Count == 0 ? Camp.Marines
                        : null;

        Console.WriteLine(vainqueur is null ? "Match nul : temps écoulé." : $"Victoire des {vainqueur} !");
        return vainqueur;
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var arene = new Arene();
        arene.Inscrire(new MarineTactique());
        arene.Inscrire(new Terminator());
        arene.Inscrire(new Scorpion());
        arene.Inscrire(new SuperMutant());

        arene.Combattre();
    }
}
