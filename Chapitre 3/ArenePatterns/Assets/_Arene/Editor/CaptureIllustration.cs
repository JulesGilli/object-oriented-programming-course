using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ArenePatterns.Editeur
{
    /// <summary>
    /// Produit l’illustration du chapitre 3 : ouvre la scène, place quelques ennemis et projectiles,
    /// rend la caméra dans une image PNG, puis referme la scène sans l’enregistrer.
    /// En ligne de commande : -executeMethod ArenePatterns.Editeur.CaptureIllustration.Capturer
    /// </summary>
    public static class CaptureIllustration
    {
        private const string Sortie = "../Images/arene-apercu.png";   // relatif au dossier du projet

        [MenuItem("Arène/Capturer une illustration")]
        public static void Capturer()
        {
            EditorSceneManager.OpenScene("Assets/_Arene/Scenes/Arene.unity", OpenSceneMode.Single);

            var prefabEnnemi = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Arene/Prefabs/Ennemi.prefab");
            var prefabProjectile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Arene/Prefabs/Projectile.prefab");
            var rapide = AssetDatabase.LoadAssetAtPath<DonneesEnnemiSO>("Assets/_Arene/Donnees/EnnemiRapide.asset");
            var costaud = AssetDatabase.LoadAssetAtPath<DonneesEnnemiSO>("Assets/_Arene/Donnees/EnnemiCostaud.asset");

            var joueur = Object.FindAnyObjectByType<Joueur>().transform;
            joueur.rotation = Quaternion.Euler(0f, 30f, 0f);

            (Vector3 position, DonneesEnnemiSO donnees)[] ennemis =
            {
                (new Vector3(5f, 0f, 7f), rapide), (new Vector3(8f, 0f, 3f), costaud),
                (new Vector3(-7f, 0f, 6f), rapide), (new Vector3(-4f, 0f, -6f), rapide),
                (new Vector3(9f, 0f, -5f), costaud), (new Vector3(-10f, 0f, 1f), rapide),
            };
            foreach (var (position, donnees) in ennemis)
            {
                var ennemi = (GameObject)PrefabUtility.InstantiatePrefab(prefabEnnemi);
                ennemi.transform.position = position + Vector3.up * donnees.Taille / 2f;
                ennemi.transform.localScale = Vector3.one * donnees.Taille;
                ennemi.transform.LookAt(new Vector3(0f, ennemi.transform.position.y, 0f));
                ennemi.GetComponent<Renderer>().sharedMaterial = donnees.Materiau;
            }

            for (int i = 1; i <= 4; i++)
            {
                var projectile = (GameObject)PrefabUtility.InstantiatePrefab(prefabProjectile);
                projectile.transform.position = new Vector3(0f, 0.6f, 0f) + Quaternion.Euler(0f, 30f, 0f) * Vector3.forward * (1.2f * i + 0.5f);
            }

            var cameraJeu = Camera.main;
            var rendu = new RenderTexture(1280, 720, 24);
            cameraJeu.targetTexture = rendu;
            for (int i = 0; i < 3; i++)      // quelques rendus d’échauffement : le premier rendu en mode batch peut être incomplet
                cameraJeu.Render();

            RenderTexture.active = rendu;
            var image = new Texture2D(rendu.width, rendu.height, TextureFormat.RGB24, false);
            image.ReadPixels(new Rect(0, 0, rendu.width, rendu.height), 0, 0);
            image.Apply();
            RenderTexture.active = null;
            cameraJeu.targetTexture = null;

            string chemin = Path.GetFullPath(Path.Combine(Application.dataPath, "..", Sortie));
            Directory.CreateDirectory(Path.GetDirectoryName(chemin));
            File.WriteAllBytes(chemin, image.EncodeToPNG());
            Debug.Log($"[Arène] Illustration enregistrée : {chemin}");

            Object.DestroyImmediate(image);
            Object.DestroyImmediate(rendu);
            EditorSceneManager.OpenScene("Assets/_Arene/Scenes/Arene.unity", OpenSceneMode.Single);   // sans enregistrer les ajouts
        }
    }
}
