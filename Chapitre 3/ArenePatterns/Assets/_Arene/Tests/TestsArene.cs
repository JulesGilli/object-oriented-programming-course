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
