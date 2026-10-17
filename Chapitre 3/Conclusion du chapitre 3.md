# Conclusion du chapitre 3 — et du cours

> 🎯 **Dans cette conclusion :**
> * tester automatiquement le jeu avec des tests **PlayMode** ;
> * faire le bilan des patterns utilisés et des problèmes qu’ils ont résolus ;
> * des pistes pour aller plus loin.

---

## Tester le jeu

Le projet contient huit tests **PlayMode** : ils chargent la vraie scène, la laissent tourner, et vérifient que chaque pattern fait son travail.

```csharp
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace ArenePatterns.Tests
{
    /// <summary>
    /// Tests PlayMode : ils chargent la vraie scène et vérifient que les patterns se comportent comme prévu.
    /// Lancement : Window > General > Test Runner > PlayMode, ou en ligne de commande avec « unity test ».
    /// </summary>
    public class TestsArene
    {
        private Joueur joueur;
        private GenerateurVagues generateur;
        private PoolProjectiles pool;
        private GestionnairePartie partie;

        [UnitySetUp]
        public IEnumerator ChargerLaScene()
        {
            yield return SceneManager.LoadSceneAsync("Arene", LoadSceneMode.Single);
            yield return null;   // laisse passer Start()

            joueur = Object.FindAnyObjectByType<Joueur>();
            generateur = Object.FindAnyObjectByType<GenerateurVagues>();
            pool = Object.FindAnyObjectByType<PoolProjectiles>();
            partie = Object.FindAnyObjectByType<GestionnairePartie>();

            generateur.Arreter();   // chaque test fait apparaître lui-même ses ennemis
        }

        [Test]
        public void La_scene_est_entierement_branchee()
        {
            Assert.IsNotNull(joueur);
            Assert.IsNotNull(generateur);
            Assert.IsNotNull(pool);
            Assert.IsNotNull(partie);
            Assert.IsNotNull(Object.FindAnyObjectByType<LecteurEntrees>());
            Assert.IsNotNull(Object.FindAnyObjectByType<Hud>());
            Assert.AreEqual(1, generateur.NumeroVague, "La première vague doit avoir commencé.");
            Assert.AreEqual(joueur.PvMax, joueur.Pv);
        }

        [UnityTest]
        public IEnumerator Une_commande_de_deplacement_deplace_le_joueur()
        {
            float xDepart = joueur.transform.position.x;
            ICommandeJoueur commande = new CommandeDeplacement(Vector2.right);

            for (int i = 0; i < 10; i++)
            {
                commande.Executer(joueur);
                yield return null;
            }

            Assert.Greater(joueur.transform.position.x, xDepart);
        }

        [Test]
        public void Le_changement_de_mode_de_tir_previent_les_abonnes()
        {
            string recu = null;
            joueur.ModeDeTirChange += mode => recu = mode.NomAffiche;

            new CommandeChangerModeDeTir(1).Executer(joueur);

            Assert.AreEqual("Tir en éventail", recu);
            Assert.AreEqual("Tir en éventail", joueur.ModeDeTir.NomAffiche);
        }

        [UnityTest]
        public IEnumerator Le_pool_recycle_les_projectiles()
        {
            for (int i = 0; i < 10; i++)
                pool.Lancer(new Vector3(0f, 0.6f, 20f), Quaternion.identity, 1);   // hors de l’arène
            int creesApresPremiereSalve = pool.NombreCrees;

            yield return new WaitForSeconds(1.5f);                                   // les projectiles reviennent au pool
            Assert.AreEqual(0, pool.NombreActifs);

            for (int i = 0; i < 10; i++)
                pool.Lancer(new Vector3(0f, 0.6f, 20f), Quaternion.identity, 1);

            Assert.AreEqual(creesApresPremiereSalve, pool.NombreCrees, "Aucun nouveau projectile ne doit être créé.");
        }

        [UnityTest]
        public IEnumerator Un_ennemi_est_invulnerable_pendant_son_apparition()
        {
            Ennemi ennemi = generateur.FaireApparaitre(generateur.CompositionDeLaVague(1).First());

            ennemi.SubirDegats(999);
            Assert.IsTrue(ennemi.EstVivant);
            Assert.AreEqual(nameof(EtatApparition), ennemi.NomEtat);

            yield return new WaitForSeconds(0.8f);
            ennemi.SubirDegats(999);

            Assert.IsFalse(ennemi.EstVivant);
            Assert.AreEqual(nameof(EtatMort), ennemi.NomEtat);
        }

        [UnityTest]
        public IEnumerator Un_ennemi_poursuit_puis_attaque_le_joueur()
        {
            Ennemi ennemi = generateur.FaireApparaitre(generateur.CompositionDeLaVague(1).First());

            float limite = Time.time + 8f;
            while (ennemi.NomEtat != nameof(EtatAttaque) && Time.time < limite)
                yield return null;

            Assert.AreEqual(nameof(EtatAttaque), ennemi.NomEtat, "L’ennemi doit atteindre le joueur.");

            yield return new WaitForSeconds(0.2f);
            Assert.Less(joueur.Pv, joueur.PvMax, "Au contact, l’ennemi doit infliger des dégâts.");
        }

        [UnityTest]
        public IEnumerator Le_hud_reflete_les_pv_et_le_score()
        {
            VisualElement racine = Object.FindAnyObjectByType<Hud>().GetComponent<UIDocument>().rootVisualElement;
            Label Texte(string nom) => racine.Q<Label>(nom);

            joueur.SubirDegats(75);
            Assert.AreEqual("25 / 100", Texte("texte-pv").text);
            Assert.IsTrue(racine.Q("barre-pv-remplissage").ClassListContains("barre__remplissage--critique"),
                          "Sous 30 % de PV, la barre doit passer en mode critique.");

            Ennemi ennemi = generateur.FaireApparaitre(generateur.CompositionDeLaVague(1).First());
            yield return new WaitForSeconds(0.8f);                 // fin de l’apparition
            ennemi.SubirDegats(999);

            Assert.AreEqual(ennemi.Donnees.Points.ToString(), Texte("texte-score").text);
        }

        [UnityTest]
        public IEnumerator La_mort_du_joueur_termine_la_partie_et_R_la_relance()
        {
            int scoreFinal = -1;
            partie.PartieTerminee += score => scoreFinal = score;

            joueur.SubirDegats(joueur.PvMax);
            yield return null;

            Assert.IsTrue(partie.EstTerminee);
            Assert.AreEqual(0, scoreFinal);

            partie.NouvellePartie();
            yield return null;

            Assert.IsFalse(partie.EstTerminee);
            Assert.AreEqual(joueur.PvMax, joueur.Pv);
        }
    }
}
```

**Pour les lancer :**

* dans l’éditeur : **Window › General › Test Runner**, onglet **PlayMode**, puis **Run All** ;
* en ligne de commande, avec la [CLI Unity](https://docs.unity3d.com/Manual/CommandLineArguments.html) :

```bash
unity test "Chapitre 3/ArenePatterns" --mode PlayMode
```

Plusieurs choix de conception du jeu rendent ces tests simples à écrire :

* **sans Singleton**, un test charge la scène et récupère les objets dont il a besoin, sans état global à remettre à zéro ;
* **grâce au pattern Command**, un test pilote le joueur sans simuler de clavier ;
* **grâce à l’Observer**, un test s’abonne à un événement pour vérifier qu’il a bien été déclenché ;
* **grâce aux noms d’états** (`NomEtat`), un test vérifie l’IA sans dépendre de détails d’animation.

---

## Le bilan

| Pattern | Problème résolu dans le jeu | Ce qu’on aurait eu sans lui |
| --- | --- | --- |
| **Command** | Plusieurs sources d’entrées (clavier, manette, IA, tests) pour un même joueur | Un joueur qui lit le clavier, impossible à piloter autrement |
| **Iterator** | Des commandes produites à la demande, des vagues étalées dans le temps | Un `Update()` rempli de minuteurs et de drapeaux |
| **Strategy** | Des modes de tir interchangeables et réglables dans l’éditeur | Un `switch` sur le type d’arme dans le joueur |
| **Object Pool** | Des centaines de projectiles sans allocations | Des saccades dues au ramasse-miettes |
| **Flyweight** | Des caractéristiques et des matériaux partagés par type d’ennemi | Des données dupliquées dans chaque ennemi, un matériau par instance |
| **State** | Une IA lisible : apparition, poursuite, attaque, mort | Un `switch` géant sur une énumération |
| **Factory** | La création d’un ennemi en un seul appel | De l’assemblage d’ennemi copié partout |
| **Observer** | Un HUD et un score découplés du reste du jeu | Des références croisées entre tous les scripts |

Et, en filigrane, les principes du chapitre 1 : **encapsulation** (PV privés, ScriptableObjects en lecture seule), **composition** plutôt qu’héritage (le joueur *a* un mode de tir), **faible couplage** (personne ne connaît le HUD), **inversion des dépendances** (les ennemis attaquent un `IEndommageable`, pas un `Joueur`).

> [!TIP]
> Notez aussi ce qui n’est **pas** dans le projet : ni Singleton, ni `FindObjectOfType` dans le code de jeu, ni `switch` sur des types. Les patterns ne servent pas qu’à ajouter de la structure : ils servent aussi à en retirer ce qui rend le code fragile.

---

## Pour aller plus loin

* **Un replay :** enregistrez chaque commande avec l’instant où elle a été exécutée, puis rejouez la liste dans une nouvelle partie. (Attention : il faudra aussi rendre déterministes les positions d’apparition, aujourd’hui tirées au hasard.)
* **Une IA de démonstration :** un second « lecteur » qui produit des commandes à partir de la position des ennemis, au lieu du clavier.
* **Un menu de pause en UI Toolkit**, avec de vrais boutons : il faudra cette fois laisser leurs éléments capter la souris (`picking-mode="Position"`) et suspendre le jeu pendant qu’il est affiché.
* **Des Event Channels en ScriptableObject** (fiche [7. Observer](<../Chapitre 2/Partie III — Patterns Comportementaux/7. Observer.md>)) pour que le HUD puisse vivre dans une scène séparée.
* **Un pool d’ennemis** : seule la `FabriqueEnnemis` et l’état `Mort` changeraient.

---

## Le mot de la fin

Vous avez parcouru tout le cours : les fondements de la programmation orientée objet, les outils du langage C#, vingt patterns de conception, deux projets et un jeu complet. Vous avez vu, à chaque étape, que ces notions ne sont pas des règles à appliquer partout, mais des **outils** : chacun répond à un problème précis, et a un coût.

Le meilleur réflexe à garder : avant d’ajouter une abstraction, demandez-vous **quel changement** elle va faciliter. S’il n’y en a pas, le code le plus simple est souvent le meilleur.

---

[← Précédent : 4. La partie et le HUD](<4. La partie et le HUD.md>) · [Sommaire](<../README.md>)
