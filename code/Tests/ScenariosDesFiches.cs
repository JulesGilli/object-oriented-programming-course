// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.
// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.

namespace CoursPOO.Tests;

/// <summary>Vérifie que chaque scénario des fiches affiche exactement la sortie annoncée.</summary>
public class ScenariosDesFiches
{
    [Fact(DisplayName = "Ch. 1 — 1. Classe et Objet")]
    public void Ch1_01_ClasseEtObjet() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_01_ClasseEtObjet.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Bonjour, je suis Kevin et j’ai 2 ans.
        Bonjour, je suis Léa et j’ai 5 ans.
        Bonjour, je suis Kevin et j’ai 3 ans.
        Bonjour, je suis Léa et j’ai 5 ans.
        """);

    [Fact(DisplayName = "Ch. 1 — 2. Attributs et Méthodes")]
    public void Ch1_02_AttributsEtMethodes() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_02_AttributsEtMethodes.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Kevin travaille. Énergie : 91
        Kevin travaille. Énergie : 82
        Kevin travaille. Énergie : 73
        Kevin travaille. Énergie : 64
        Kevin travaille. Énergie : 55
        Kevin travaille. Énergie : 46
        Kevin travaille. Énergie : 37
        Kevin travaille. Énergie : 28
        Kevin travaille. Énergie : 19
        Kevin est trop fatigué pour travailler.
        Kevin mange. Énergie : 29
        Kevin travaille. Énergie : 20
        Geckos créés : 2
        """);

    [Fact(DisplayName = "Ch. 1 — 3. Constructeurs")]
    public void Ch1_03_Constructeurs() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_03_Constructeurs.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Bonjour Inconnu !
        Bonjour Kevin !
        Bonjour Léa !
        Bonjour Bug !
        Inconnu a 0 ans.
        Kevin a 0 ans.
        Léa a 5 ans.
        Bug a 0 ans.
        """);

    [Fact(DisplayName = "Ch. 1 — 4. Encapsulation")]
    public void Ch1_04_Encapsulation() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_04_Encapsulation.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Après 30 dégâts : 70
        Après un soin de 50 : 100
        Après 150 dégâts : 0, mort : True
        Après un soin de 20 : 0
        Dégâts négatifs refusés.
        """);

    [Fact(DisplayName = "Ch. 1 — 5. Abstraction")]
    public void Ch1_05_Abstraction() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_05_Abstraction.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Fusil à plasma : Bzziou ! (21 dégâts, 5 énergie)
        Gant énergétique : Vrrr… BAM ! (50 dégâts, 10 énergie)
        Fusil à plasma rechargé : 3 cellules.
        """);

    [Fact(DisplayName = "Ch. 1 — 6. Héritage")]
    public void Ch1_06_Heritage() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_06_Heritage.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Félix est né !
        Jaws est né !
        Je suis Félix et j’ai 4 pattes.
        Miaou.
        Je suis Jaws et j’ai 0 pattes.
        Jaws mange Félix.
        Jaws ne peut pas manger Jaws.
        """);

    [Fact(DisplayName = "Ch. 1 — 7. Polymorphisme")]
    public void Ch1_07_Polymorphisme() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_07_Polymorphisme.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Conan crie « Pour l’honneur ! » et inflige 12 dégâts.
        Merlin crie « Abracadabra ! » et inflige 15 dégâts.
        Robin crie « … » et inflige 10 dégâts.
        Dégâts totaux : 37
        """);

    [Fact(DisplayName = "Ch. 1 — 10. Couplage et Cohésion")]
    public void Ch1_10_CouplageEtCohesion() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_10_CouplageEtCohesion.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        === Rapport de partie ===
        Score : 1300
        Rang : Or
        Argent
        """);

    [Fact(DisplayName = "Ch. 1 — 11. Principes SOLID")]
    public void Ch1_11_PrincipesSOLID() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_11_PrincipesSOLID.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        [Journal] Arthur a acheté Potion pour 10 pièces d’or.
        True
        False
        Or restant : 20
        """);

    [Fact(DisplayName = "Ch. 1 — 8. Composition vs héritage")]
    public void Ch1_08_CompositionVsHeritage() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_08_CompositionVsHeritage.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Arthur marche et donne un coup d’épée.
        Arthur marche et tire une flèche.
        Arthur nage et tire une flèche.
        Smaug vole et donne un coup d’épée.
        """);

    [Fact(DisplayName = "Ch. 1 — 12. Types valeur et types référence")]
    public void Ch1_12_TypesValeurEtTypesReference() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_12_TypesValeurEtTypesReference.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        5 6
        70 70
        42 5
        True
        True
        """);

    [Fact(DisplayName = "Ch. 1 — 13. Membres statiques, énumérations et namespaces")]
    public void Ch1_13_MembresStatiquesEnumerationsEtNamespaces() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_13_MembresStatiquesEnumerationsEtNamespaces.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Gobelin : 3
        Orc : 1
        Dragon : 0
        PV totaux : 170
        """);

    [Fact(DisplayName = "Ch. 1 — 14. Exceptions")]
    public void Ch1_14_Exceptions() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_14_Exceptions.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Croissants restants : 1
        True
        False
        Erreur : Plus de « Baguette » en stock.
        Erreur : quantite
        """);

    [Fact(DisplayName = "Ch. 1 — 15. Génériques")]
    public void Ch1_15_Generiques() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_15_Generiques.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Merlin (45)
        9
        pomme
        """);

    [Fact(DisplayName = "Ch. 1 — 16. Délégués, événements et lambdas")]
    public void Ch1_16_DeleguesEvenementsEtLambdas() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_16_DeleguesEvenementsEtLambdas.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Il reste 2 s
        Boum !
        """);

    [Fact(DisplayName = "Ch. 1 — Projet — Arène spatiale")]
    public void Ch1_Projet_AreneSpatiale() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch1_Projet_AreneSpatiale.Scenario.Executer, normaliserNumeros: true, attendu:
        """
        Marine #1 entre dans l’arène.
        Terminator #1 entre dans l’arène.
        Scorpion #1 entre dans l’arène.
        Super-mutant #1 entre dans l’arène.
        --- Tour 1 ---
        Marine #1 attaque Scorpion #1 avec son fusil à plasma : Bzziou !
          Scorpion #1 perd 21 PV (59/80)
        Scorpion #1 attaque Marine #1 avec ses griffes : Shrrk !
          Marine #1 perd 11 PV (89/100)
        --- Tour 2 ---
        Marine #1 attaque Scorpion #1 avec son fusil à plasma : Bzziou !
          Scorpion #1 perd 21 PV (38/80)
        Scorpion #1 attaque Marine #1 avec ses griffes : Shrrk !
          Marine #1 perd 11 PV (78/100)
        --- Tour 3 ---
        Marine #1 attaque Scorpion #1 avec son fusil à plasma : Bzziou !
          Scorpion #1 perd 21 PV (17/80)
        Scorpion #1 attaque Marine #1 avec ses griffes : Shrrk !
          Marine #1 perd 11 PV (67/100)
        --- Tour 4 ---
        Marine #1 attaque Scorpion #1 avec son fusil à plasma : Bzziou !
          Scorpion #1 perd 21 PV (0/80)
          Scorpion #1 est éliminé !
        Super-mutant #1 attaque Marine #1 avec sa massue : Bonk !
          Marine #1 perd 27 PV (40/100)
        --- Tour 5 ---
        Marine #1 attaque Super-mutant #1 avec son fusil à plasma : Bzziou !
          Super-mutant #1 perd 19 PV (151/170)
        Super-mutant #1 attaque Marine #1 avec sa massue : Bonk !
          Marine #1 perd 27 PV (13/100)
        --- Tour 6 ---
        Marine #1 attaque Super-mutant #1 avec son fusil à plasma : Bzziou !
          Super-mutant #1 perd 19 PV (132/170)
        Super-mutant #1 attaque Marine #1 avec sa massue : Bonk !
          Marine #1 perd 27 PV (0/100)
          Marine #1 est éliminé !
        --- Tour 7 ---
        Terminator #1 attaque Super-mutant #1 avec son gant énergétique : Vrrr… BAM !
          Super-mutant #1 perd 48 PV (84/170)
        Super-mutant #1 attaque Terminator #1 avec sa massue : Bonk !
          Terminator #1 perd 24 PV (126/150)
        --- Tour 8 ---
        Terminator #1 attaque Super-mutant #1 avec son gant énergétique : Vrrr… BAM !
          Super-mutant #1 perd 48 PV (36/170)
        Super-mutant #1 est à bout de souffle (4 PA).
        --- Tour 9 ---
        Terminator #1 attaque Super-mutant #1 avec son gant énergétique : Vrrr… BAM !
          Super-mutant #1 perd 48 PV (0/170)
          Super-mutant #1 est éliminé !
        Victoire des Marines !
        """);

    [Fact(DisplayName = "Ch. 2 — 1. Singleton")]
    public void Ch2_01_Singleton() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_01_Singleton.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Instances avant le premier accès : 0
        Même objet : True
        Volume vu par b : 80
        Instances après : 1
        """);

    [Fact(DisplayName = "Ch. 2 — 2. Factory Method")]
    public void Ch2_02_FactoryMethod() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_02_FactoryMethod.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Contrôle qualité : Ours en peluche… OK
        Emballage : Ours en peluche
        L’enfant joue : Câlin !
        Contrôle qualité : Console portable… OK
        Emballage : Console portable
        L’enfant joue : Bip bip !
        """);

    [Fact(DisplayName = "Ch. 2 — 3. Builder")]
    public void Ch2_03_Builder() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_03_Builder.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Quête : Le dragon du nord (500 pièces d’or)
          1. Trouver la caverne
          2. Vaincre le dragon
        Erreur : Une quête doit avoir au moins un objectif.
        """);

    [Fact(DisplayName = "Ch. 2 — 4. Adapter")]
    public void Ch2_04_Adapter() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_04_Adapter.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        [Ancien système] Arthur : 001250
        [Ancien système] Morgane : 000980
        [Ancien système] Lancelot : 000015
        Meilleur score : 1250
        """);

    [Fact(DisplayName = "Ch. 2 — 5. Decorator")]
    public void Ch2_05_Decorator() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_05_Decorator.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Potion de soin épicée concentrée : 50 PV
        Potion de soin concentrée épicée : 45 PV
        """);

    [Fact(DisplayName = "Ch. 2 — 6. Facade")]
    public void Ch2_06_Facade() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_06_Facade.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Chargement du niveau 4
        Audio : Thème du niveau 4
        Sauvegarde : niveau 1
        Chargement du niveau 1
        Audio : Thème principal
        """);

    [Fact(DisplayName = "Ch. 2 — 10. Command")]
    public void Ch2_10_Command() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_10_Command.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Après 3 déplacements : (4, 2)
        Après 2 annulations : (1, 0)
        Après « refaire » : (1, 2)
        Après un nouveau déplacement : (1, -3)
        """);

    [Fact(DisplayName = "Ch. 2 — 7. Observer")]
    public void Ch2_07_Observer() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_07_Observer.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        [Alice] Commande n°42 : Expédiée
        [Bob] Commande n°42 : Expédiée
        [Alice] Commande n°42 : Livrée
        """);

    [Fact(DisplayName = "Ch. 2 — 8. Strategy")]
    public void Ch2_08_Strategy() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_08_Strategy.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        La tourelle vise Orc (PlusProche).
        La tourelle vise Ogre (PlusResistant).
        La tourelle vise Rat (PlusFaible).
        """);

    [Fact(DisplayName = "Ch. 2 — 9. State")]
    public void Ch2_09_State() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_09_State.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        La porte s’ouvre.
        Impossible de verrouiller une porte ouverte.
        La porte se ferme.
        Clic ! La porte est verrouillée.
        La porte est verrouillée.
        Clac ! La porte est déverrouillée.
        La porte s’ouvre.
        """);

    [Fact(DisplayName = "Ch. 2 — 11. Template Method")]
    public void Ch2_11_TemplateMethod() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_11_TemplateMethod.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        == Baguette ==
        Pétrir la pâte.
        Repos de la pâte : 1 h.
        Cuire 25 min à 240 °C.
        Prêt !
        == Cookie ==
        Mélanger beurre, sucre et pépites.
        Cuire 10 min à 180 °C.
        Prêt !
        """);

    [Fact(DisplayName = "Ch. 2 — 12. Composite")]
    public void Ch2_12_Composite() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_12_Composite.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Le dragon crache du feu
        5 mots
        """);

    [Fact(DisplayName = "Ch. 2 — 13. Prototype")]
    public void Ch2_13_Prototype() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_13_Prototype.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Arthur niv. 5 : Épée
        Arthur (illusion) niv. 1 : Épée, Miroir
        """);

    [Fact(DisplayName = "Ch. 2 — 14. Object Pool")]
    public void Ch2_14_ObjectPool() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_14_ObjectPool.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Disponibles : 2, détruits : 1
        Erreur : Cet objet a déjà été rendu au pool.
        """);

    [Fact(DisplayName = "Ch. 2 — 15. Flyweight")]
    public void Ch2_15_Flyweight() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_15_Flyweight.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Chêne (vert) en (0, 0)
        Sapin (vert foncé) en (3, 1)
        Chêne (vert) en (5, 2)
        Chêne (roux) en (7, 4)
        Sapin (vert foncé) en (9, 9)
        Chêne (vert) en (2, 8)
        6 arbres, 3 espèces en mémoire
        """);

    [Fact(DisplayName = "Ch. 2 — 16. Mediator")]
    public void Ch2_16_Mediator() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_16_Mediator.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Ana rejoint le salon (1/3).
        Ben rejoint le salon (2/3).
        Chloé rejoint le salon (3/3).
        Ana a cliqué sur « Prêt » (1/3).
        Chloé a cliqué sur « Prêt » (2/3).
        Partie lancée : False
        Ben a cliqué sur « Prêt » (3/3).
        Tout le monde est prêt : la partie commence !
        Partie lancée : True
        """);

    [Fact(DisplayName = "Ch. 2 — 17. Iterator")]
    public void Ch2_17_Iterator() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_17_Iterator.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        Ayla, Bran, Cyd
        Ayla, Cyd
        Ayla → Cyd → Ayla → Cyd → Ayla
        Tours possibles quand tout le monde est KO : 0
        """);

    [Fact(DisplayName = "Ch. 2 — Projet — Boulangerie")]
    public void Ch2_Projet_Boulangerie() => SortieConsole.Verifier(
        CoursPOO.Exercices.Ch2_Projet_Boulangerie.Scenario.Executer, normaliserNumeros: false, attendu:
        """
        [Alerte stock] Plus que 0 × Sirop
        [Alerte stock] Plus que 1 × Croissant
        [Alerte stock] Plus que 1 × Jambon-beurre
        Impossible : Rupture de stock : Sirop.
        Impossible : Produit inconnu : « Baguette ».
        ===== Ticket =====
        Petit-déjeuner (Café grand format + sirop, Croissant) : 3,15 €
        Croissant : 1,20 €
        Déjeuner (Jus d’orange, Jambon-beurre) : 6,75 €
        Remise étudiant (-15 %)
        Total : 9,43 € (1044 kcal)
        Cafés restants : 9
        """);
}
