using Encapsulation = CoursPOO.Exercices.Ch1_04_Encapsulation;
using Exceptions = CoursPOO.Exercices.Ch1_14_Exceptions;
using Arene = CoursPOO.Exercices.Ch1_Projet_AreneSpatiale;
using Builder = CoursPOO.Exercices.Ch2_03_Builder;
using Decorator = CoursPOO.Exercices.Ch2_05_Decorator;
using State = CoursPOO.Exercices.Ch2_09_State;
using Command = CoursPOO.Exercices.Ch2_10_Command;
using Flyweight = CoursPOO.Exercices.Ch2_15_Flyweight;
using Iterator = CoursPOO.Exercices.Ch2_17_Iterator;
using Boulangerie = CoursPOO.Exercices.Ch2_Projet_Boulangerie;

namespace CoursPOO.Tests;

/// <summary>
/// Tests écrits à la main : ils vérifient les règles importantes des exercices et des projets,
/// au-delà des sorties console.
/// </summary>
public class ReglesMetier
{
    // ---------- Chapitre 1

    [Theory]
    [InlineData(30, 70)]
    [InlineData(100, 0)]
    [InlineData(250, 0)]     // jamais négatif
    public void BarreDeVie_reste_entre_0_et_PvMax(int degats, int pvAttendus)
    {
        var vie = new Encapsulation.BarreDeVie(100);
        vie.SubirDegats(degats);
        Assert.Equal(pvAttendus, vie.Pv);
    }

    [Fact]
    public void BarreDeVie_ne_soigne_ni_au_dela_du_maximum_ni_un_mort()
    {
        var vie = new Encapsulation.BarreDeVie(100);
        vie.SubirDegats(10);
        vie.Soigner(500);
        Assert.Equal(100, vie.Pv);

        vie.SubirDegats(100);
        vie.Soigner(50);
        Assert.True(vie.EstMort);
    }

    [Fact]
    public void BarreDeVie_refuse_les_valeurs_negatives()
    {
        var vie = new Encapsulation.BarreDeVie(100);
        Assert.Throws<ArgumentOutOfRangeException>(() => vie.SubirDegats(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => vie.Soigner(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Encapsulation.BarreDeVie(0));
    }

    [Fact]
    public void Stock_des_exceptions_ne_devient_jamais_negatif()
    {
        var stock = new Exceptions.Stock();
        stock.Ajouter("Croissant", 1);

        Assert.True(stock.TryPrendre("Croissant"));
        Assert.False(stock.TryPrendre("Croissant"));
        Assert.Throws<Exceptions.ProduitIndisponibleException>(() => stock.Prendre("Croissant"));
        Assert.Equal(0, stock.Quantite("Croissant"));
    }

    [Fact]
    public void Arene_numerote_les_unites_par_type()
    {
        var premier = new Arene.Scorpion();
        var second = new Arene.Scorpion();

        int Numero(Arene.Unite u) => int.Parse(u.Nom.Split('#')[1]);
        Assert.Equal(Numero(premier) + 1, Numero(second));
    }

    [Fact]
    public void Arene_l_armure_absorbe_les_degats_mais_au_moins_1_PV_est_perdu()
    {
        var terminator = new Arene.Terminator();          // armure 6
        SortieConsole.Capturer(() => terminator.RecevoirDegats(4));
        Assert.Equal(149, terminator.Pv);

        SortieConsole.Capturer(() => terminator.RecevoirDegats(20));
        Assert.Equal(135, terminator.Pv);
    }

    [Fact]
    public void Arene_une_unite_sans_PA_ne_peut_pas_attaquer()
    {
        var attaquant = new Arene.MarineTactique();       // 20 PA, 5 par tir
        var cible = new Arene.SuperMutant();

        bool[] resultats = new bool[5];
        SortieConsole.Capturer(() =>
        {
            for (int i = 0; i < 5; i++)
                resultats[i] = attaquant.Attaquer(cible);
        });

        Assert.Equal(new[] { true, true, true, true, false }, resultats);
        Assert.Equal(0, attaquant.Pa);
    }

    // ---------- Chapitre 2

    [Fact]
    public void Builder_refuse_une_quete_sans_objectif()
    {
        var builder = new Builder.QueteBuilder().AvecTitre("Quête vide");
        Assert.Throws<InvalidOperationException>(() => builder.Construire());
    }

    [Fact]
    public void Builder_produit_des_quetes_independantes()
    {
        var builder = new Builder.QueteBuilder().AvecTitre("Q").AjouterObjectif("A");
        var premiere = builder.Construire();
        builder.AjouterObjectif("B");
        var seconde = builder.Construire();

        Assert.Single(premiere.Objectifs);
        Assert.Equal(2, seconde.Objectifs.Count);
    }

    [Fact]
    public void Decorator_l_ordre_des_ingredients_compte()
    {
        var p1 = new Decorator.Concentree(new Decorator.Epicee(new Decorator.PotionDeSoin()));
        var p2 = new Decorator.Epicee(new Decorator.Concentree(new Decorator.PotionDeSoin()));

        Assert.Equal(50, p1.Puissance);
        Assert.Equal(45, p2.Puissance);
    }

    [Fact]
    public void State_une_porte_verrouillee_ne_s_ouvre_pas()
    {
        var porte = new State.Porte();
        string sortie = SortieConsole.Capturer(() =>
        {
            porte.Verrouiller();
            porte.Ouvrir();
        });

        Assert.EndsWith("La porte est verrouillée.", sortie);
    }

    [Fact]
    public void Command_annuler_puis_refaire_revient_au_meme_etat()
    {
        var robot = new Command.Robot();
        var gestionnaire = new Command.GestionnaireCommandes();

        gestionnaire.ExecuterCommande(new Command.CommandeDeplacement(robot, 2, 3));
        gestionnaire.Annuler();
        Assert.Equal((0, 0), (robot.X, robot.Y));

        gestionnaire.Refaire();
        Assert.Equal((2, 3), (robot.X, robot.Y));
    }

    [Fact]
    public void Flyweight_les_especes_identiques_sont_partagees()
    {
        var foret = new Flyweight.Foret();
        for (int i = 0; i < 100; i++)
            foret.Planter(i, i, "Chêne", "vert");

        Assert.Equal(100, foret.NombreArbres);
        Assert.Equal(1, foret.NombreEspeces);
    }

    [Fact]
    public void Iterator_le_tour_de_jeu_ignore_les_heros_KO()
    {
        var equipe = new Iterator.Equipe();
        equipe.Ajouter(new Iterator.Heros("A", 10));
        equipe.Ajouter(new Iterator.Heros("B", 0));

        var noms = equipe.TourDeJeu().Take(3).Select(h => h.Nom);
        Assert.Equal(new[] { "A", "A", "A" }, noms);
    }

    [Fact]
    public void Boulangerie_un_prelevement_est_tout_ou_rien()
    {
        var stock = new Boulangerie.Stock(seuilAlerte: 0);
        stock.Approvisionner("Café", 1);

        var menu = new Boulangerie.PetitDejeuner(new Boulangerie.Cafe(), new Boulangerie.Croissant());   // pas de croissant en stock
        Assert.Throws<Boulangerie.RuptureDeStockException>(() => stock.Prelever(menu.ArticlesAPrelever()));
        Assert.Equal(1, stock.Quantite("Café"));   // le café n’a pas été retiré
    }

    [Fact]
    public void Boulangerie_la_remise_etudiant_arrondit_au_centime_inferieur()
    {
        Assert.Equal(943, new Boulangerie.RemiseEtudiant().Appliquer(1110));
        Assert.Equal(1110, new Boulangerie.SansRemise().Appliquer(1110));
    }

    [Fact]
    public void Boulangerie_le_stock_previent_quand_il_devient_bas()
    {
        var stock = new Boulangerie.Stock(seuilAlerte: 1);
        stock.Approvisionner("Croissant", 2);
        var alertes = new List<string>();
        stock.StockBas += (article, reste) => alertes.Add($"{article}:{reste}");

        stock.Prelever(new[] { "Croissant" });

        Assert.Equal(new[] { "Croissant:1" }, alertes);
    }
}
