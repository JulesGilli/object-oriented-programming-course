// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie II — Les 4 piliers de la POO/6. Héritage.md

namespace CoursPOO.Exercices.Ch1_06_Heritage;

public class Animal
{
    public string Nom { get; }
    protected int NombreDePattes { get; }

    protected Animal(string nom, int nombreDePattes)
    {
        Nom = nom;
        NombreDePattes = nombreDePattes;
        Console.WriteLine($"{Nom} est né !");
    }

    public virtual void SeDecrire()
    {
        Console.WriteLine($"Je suis {Nom} et j’ai {NombreDePattes} pattes.");
    }

    public virtual bool PeutManger(Animal proie) => false;
}

public class Chat : Animal
{
    public Chat(string nom) : base(nom, 4) { }

    public override void SeDecrire()
    {
        base.SeDecrire();          // étend le comportement au lieu de le remplacer
        Console.WriteLine("Miaou.");
    }
}

public class Requin : Animal
{
    public Requin(string nom) : base(nom, 0) { }

    public override bool PeutManger(Animal proie) => proie != this;

    public void Manger(Animal proie)
    {
        if (PeutManger(proie))
            Console.WriteLine($"{Nom} mange {proie.Nom}.");
        else
            Console.WriteLine($"{Nom} ne peut pas manger {proie.Nom}.");
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var felix = new Chat("Félix");
        var jaws = new Requin("Jaws");

        felix.SeDecrire();
        jaws.SeDecrire();

        jaws.Manger(felix);
        jaws.Manger(jaws);
    }
}
