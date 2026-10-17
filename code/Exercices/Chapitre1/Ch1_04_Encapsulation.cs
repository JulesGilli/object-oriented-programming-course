// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.
// Source : Chapitre 1/Partie II — Les 4 piliers de la POO/4. Encapsulation.md

namespace CoursPOO.Exercices.Ch1_04_Encapsulation;

public class BarreDeVie
{
    public int PvMax { get; }
    public int Pv { get; private set; }
    public bool EstMort => Pv == 0;

    public BarreDeVie(int pvMax)
    {
        if (pvMax <= 0)
            throw new ArgumentOutOfRangeException(nameof(pvMax), "Les PV maximum doivent être positifs.");

        PvMax = pvMax;
        Pv = pvMax;
    }

    public void SubirDegats(int degats)
    {
        if (degats < 0)
            throw new ArgumentOutOfRangeException(nameof(degats), "Les dégâts ne peuvent pas être négatifs.");

        Pv = Math.Max(0, Pv - degats);
    }

    public void Soigner(int soin)
    {
        if (soin < 0)
            throw new ArgumentOutOfRangeException(nameof(soin), "Le soin ne peut pas être négatif.");

        if (EstMort)
            return;   // on ne ressuscite pas avec un simple soin

        Pv = Math.Min(PvMax, Pv + soin);
    }
}

public static class Scenario
{
    public static void Executer()
    {
        var vie = new BarreDeVie(100);

        vie.SubirDegats(30);
        Console.WriteLine($"Après 30 dégâts : {vie.Pv}");

        vie.Soigner(50);
        Console.WriteLine($"Après un soin de 50 : {vie.Pv}");

        vie.SubirDegats(150);
        Console.WriteLine($"Après 150 dégâts : {vie.Pv}, mort : {vie.EstMort}");

        vie.Soigner(20);
        Console.WriteLine($"Après un soin de 20 : {vie.Pv}");

        try
        {
            vie.SubirDegats(-10);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Dégâts négatifs refusés.");
        }

        // vie.Pv = 1000;   // ne doit pas compiler
    }
}
