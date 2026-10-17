using System.Collections;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace ArenePatterns.Tests
{
    /// <summary>
    /// Produit l’illustration du chapitre 3 (Chapitre 3/Images/arene-apercu.png), interface comprise.
    /// Ce n’est pas un vrai test : il est marqué [Explicit] et ne s’exécute que si on le demande :
    ///   unity test "Chapitre 3/ArenePatterns" --mode PlayMode --filter CaptureIllustration
    /// </summary>
    public class CaptureIllustration
    {
        [UnityTest, Explicit("Génère l’image du chapitre 3 ; à lancer à la demande.")]
        public IEnumerator Capturer_l_illustration()
        {
            yield return SceneManager.LoadSceneAsync("Arene", LoadSceneMode.Single);
            yield return null;

            var joueur = Object.FindAnyObjectByType<Joueur>();
            var generateur = Object.FindAnyObjectByType<GenerateurVagues>();
            var document = Object.FindAnyObjectByType<Hud>().GetComponent<UIDocument>();
            var cameraJeu = Camera.main;

            // Une scène de démonstration : quelques ennemis, un ennemi déjà vaincu, un joueur blessé
            generateur.Arreter();
            Random.InitState(7);
            var ennemis = generateur.CompositionDeLaVague(3).Take(6).Select(generateur.FaireApparaitre).ToList();
            yield return new WaitForSeconds(0.9f);
            ennemis[0].SubirDegats(999);
            joueur.SubirDegats(38);
            joueur.transform.rotation = Quaternion.Euler(0f, 35f, 0f);
            new CommandeChangerModeDeTir(1).Executer(joueur);
            joueur.Tirer();
            // (la vague 1, annoncée au chargement de la scène, est encore affichée au centre)
            yield return new WaitForSeconds(0.12f);

            // La caméra puis l’interface sont rendues dans la même texture
            var rendu = new RenderTexture(1280, 720, 24);
            cameraJeu.targetTexture = rendu;
            document.panelSettings.targetTexture = rendu;
            try
            {
                // WaitForEndOfFrame n’existe pas en mode batch : on laisse passer deux frames complètes
                yield return null;
                yield return null;

                RenderTexture.active = rendu;
                var image = new Texture2D(rendu.width, rendu.height, TextureFormat.RGB24, false);
                image.ReadPixels(new Rect(0, 0, rendu.width, rendu.height), 0, 0);
                image.Apply();
                RenderTexture.active = null;

                string chemin = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "..", "Images", "arene-apercu.png"));
                Directory.CreateDirectory(Path.GetDirectoryName(chemin));
                File.WriteAllBytes(chemin, image.EncodeToPNG());
                Debug.Log($"[Arène] Illustration enregistrée : {chemin}");
                Object.Destroy(image);
            }
            finally
            {
                // PanelSettings est un asset : on ne laisse jamais la texture de capture enregistrée dedans
                cameraJeu.targetTexture = null;
                document.panelSettings.targetTexture = null;
                rendu.Release();
            }
        }
    }
}
