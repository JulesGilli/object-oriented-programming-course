using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace ArenePatterns.Editeur
{
    /// <summary>
    /// Construit toute la scène de jeu à partir de rien : matériaux, données, prefabs et scène.
    /// Menu : Arène > Construire la scène. En ligne de commande : -executeMethod ArenePatterns.Editeur.ConstructeurArene.Construire
    /// Toutes les dépendances sont branchées ici, dans l’Inspector : le jeu n’utilise aucun Singleton.
    /// </summary>
    public static class ConstructeurArene
    {
        private const string Racine = "Assets/_Arene";
        private const string CheminScene = Racine + "/Scenes/Arene.unity";

        [MenuItem("Arène/Construire la scène")]
        public static void Construire()
        {
            // La scène est créée EN PREMIER : ouvrir une nouvelle scène décharge les assets inutilisés,
            // ce qui invaliderait les références vers des assets chargés avant.
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            foreach (string dossier in new[] { "Materiaux", "Donnees", "Prefabs", "Scenes", "Interface" })
                if (!AssetDatabase.IsValidFolder($"{Racine}/{dossier}"))
                    AssetDatabase.CreateFolder(Racine, dossier);

            // ---------- Matériaux
            Material sol = CreerMateriau("Sol", new Color(0.32f, 0.34f, 0.38f));
            Material bleu = CreerMateriau("Joueur", new Color(0.2f, 0.55f, 1f));
            Material jaune = CreerMateriau("Projectile", new Color(1f, 0.85f, 0.2f));
            Material rouge = CreerMateriau("EnnemiRapide", new Color(0.9f, 0.25f, 0.25f));
            Material violet = CreerMateriau("EnnemiCostaud", new Color(0.55f, 0.3f, 0.85f));

            // ---------- Données (Strategy et Flyweight)
            var tirSimple = CreerAsset<TirSimpleSO>("TirSimple", so =>
            {
                so.FindProperty("nomAffiche").stringValue = "Tir simple";
                so.FindProperty("cadence").floatValue = 0.18f;
                so.FindProperty("degats").intValue = 10;
            });
            var tirEventail = CreerAsset<TirEnEventailSO>("TirEnEventail", so =>
            {
                so.FindProperty("nomAffiche").stringValue = "Tir en éventail";
                so.FindProperty("cadence").floatValue = 0.55f;
                so.FindProperty("degats").intValue = 8;
                so.FindProperty("nombreDeProjectiles").intValue = 5;
                so.FindProperty("angleTotal").floatValue = 50f;
            });
            var rapide = CreerAsset<DonneesEnnemiSO>("EnnemiRapide", so =>
            {
                so.FindProperty("nomAffiche").stringValue = "Rapide";
                so.FindProperty("pvMax").intValue = 20;
                so.FindProperty("vitesse").floatValue = 4.5f;
                so.FindProperty("degatsContact").intValue = 8;
                so.FindProperty("intervalleAttaque").floatValue = 0.8f;
                so.FindProperty("points").intValue = 10;
                so.FindProperty("taille").floatValue = 0.8f;
                so.FindProperty("materiau").objectReferenceValue = rouge;
            });
            var costaud = CreerAsset<DonneesEnnemiSO>("EnnemiCostaud", so =>
            {
                so.FindProperty("nomAffiche").stringValue = "Costaud";
                so.FindProperty("pvMax").intValue = 60;
                so.FindProperty("vitesse").floatValue = 2.2f;
                so.FindProperty("degatsContact").intValue = 20;
                so.FindProperty("intervalleAttaque").floatValue = 1.2f;
                so.FindProperty("points").intValue = 30;
                so.FindProperty("taille").floatValue = 1.4f;
                so.FindProperty("materiau").objectReferenceValue = violet;
            });

            // ---------- Prefabs
            Projectile prefabProjectile = CreerPrefabProjectile(jaune);
            Ennemi prefabEnnemi = CreerPrefabEnnemi(rouge);

            // ---------- Scène

            var lumiere = new GameObject("Lumière", typeof(Light)).GetComponent<Light>();
            lumiere.type = LightType.Directional;
            lumiere.intensity = 1.2f;
            lumiere.transform.rotation = Quaternion.Euler(55f, -30f, 0f);

            var cameraJeu = new GameObject("Caméra", typeof(Camera), typeof(AudioListener)).GetComponent<Camera>();
            cameraJeu.tag = "MainCamera";
            cameraJeu.transform.SetPositionAndRotation(new Vector3(0f, 24f, -16f), Quaternion.Euler(56f, 0f, 0f));
            cameraJeu.clearFlags = CameraClearFlags.SolidColor;
            cameraJeu.backgroundColor = new Color(0.08f, 0.09f, 0.12f);

            var plan = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plan.name = "Sol";
            plan.transform.localScale = new Vector3(3f, 1f, 3f);       // 30 × 30 mètres
            plan.GetComponent<Renderer>().sharedMaterial = sol;

            var joueur = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            joueur.name = "Joueur";
            joueur.transform.position = new Vector3(0f, 1f, 0f);
            joueur.GetComponent<Renderer>().sharedMaterial = bleu;
            joueur.AddComponent<Rigidbody>().isKinematic = true;
            var canon = new GameObject("Canon").transform;
            canon.SetParent(joueur.transform, false);
            canon.localPosition = new Vector3(0f, -0.4f, 0.9f);
            var composantJoueur = joueur.AddComponent<Joueur>();

            var pool = new GameObject("Pool de projectiles").AddComponent<PoolProjectiles>();
            var fabrique = new GameObject("Fabrique d’ennemis").AddComponent<FabriqueEnnemis>();
            var generateur = new GameObject("Générateur de vagues").AddComponent<GenerateurVagues>();
            var partie = new GameObject("Gestionnaire de partie").AddComponent<GestionnairePartie>();
            var entrees = new GameObject("Lecteur d’entrées").AddComponent<LecteurEntrees>();
            // L’interface en UI Toolkit : un UIDocument affiche Hud.uxml avec les réglages du PanelSettings
            var objetHud = new GameObject("HUD");
            var document = objetHud.AddComponent<UIDocument>();
            document.panelSettings = CreerPanelSettings();
            document.visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{Racine}/Interface/Hud.uxml");
            var hud = objetHud.AddComponent<Hud>();

            // ---------- Injection des dépendances (champs [SerializeField] privés)
            Brancher(composantJoueur, so =>
            {
                so.FindProperty("canon").objectReferenceValue = canon;
                so.FindProperty("projectiles").objectReferenceValue = pool;
                var modes = so.FindProperty("modesDeTir");
                modes.arraySize = 2;
                modes.GetArrayElementAtIndex(0).objectReferenceValue = tirSimple;
                modes.GetArrayElementAtIndex(1).objectReferenceValue = tirEventail;
            });
            Brancher(pool, so => so.FindProperty("prefab").objectReferenceValue = prefabProjectile);
            Brancher(fabrique, so =>
            {
                so.FindProperty("prefab").objectReferenceValue = prefabEnnemi;
                so.FindProperty("cible").objectReferenceValue = joueur.transform;
            });
            Brancher(generateur, so =>
            {
                so.FindProperty("fabrique").objectReferenceValue = fabrique;
                so.FindProperty("ennemiRapide").objectReferenceValue = rapide;
                so.FindProperty("ennemiCostaud").objectReferenceValue = costaud;
            });
            Brancher(partie, so =>
            {
                so.FindProperty("joueur").objectReferenceValue = composantJoueur;
                so.FindProperty("generateur").objectReferenceValue = generateur;
            });
            Brancher(entrees, so =>
            {
                so.FindProperty("joueur").objectReferenceValue = composantJoueur;
                so.FindProperty("cameraJeu").objectReferenceValue = cameraJeu;
            });
            Brancher(hud, so =>
            {
                so.FindProperty("joueur").objectReferenceValue = composantJoueur;
                so.FindProperty("partie").objectReferenceValue = partie;
                so.FindProperty("generateur").objectReferenceValue = generateur;
            });

            EditorSceneManager.SaveScene(scene, CheminScene);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(CheminScene, true) };
            AssetDatabase.SaveAssets();
            Debug.Log($"[Arène] Scène construite : {CheminScene}");
        }

        private static Material CreerMateriau(string nom, Color couleur)
        {
            string chemin = $"{Racine}/Materiaux/{nom}.mat";
            var materiau = AssetDatabase.LoadAssetAtPath<Material>(chemin);
            if (materiau == null)
            {
                // Le matériau par défaut du pipeline actif (URP ici) : le projet reste valable avec un autre pipeline
                Material modele = GraphicsSettings.currentRenderPipeline != null
                    ? GraphicsSettings.currentRenderPipeline.defaultMaterial
                    : new Material(Shader.Find("Standard"));
                materiau = new Material(modele);
                AssetDatabase.CreateAsset(materiau, chemin);
            }

            materiau.color = couleur;                       // _BaseColor en URP, _Color en Built-in
            EditorUtility.SetDirty(materiau);
            return materiau;
        }

        private static T CreerAsset<T>(string nom, System.Action<SerializedObject> configurer) where T : ScriptableObject
        {
            string chemin = $"{Racine}/Donnees/{nom}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<T>(chemin);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, chemin);
            }

            Brancher(asset, configurer);
            return asset;
        }

        private static void Brancher(Object cible, System.Action<SerializedObject> configurer)
        {
            var so = new SerializedObject(cible);
            configurer(so);
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static PanelSettings CreerPanelSettings()
        {
            string chemin = $"{Racine}/Interface/PanneauHud.asset";
            var reglages = AssetDatabase.LoadAssetAtPath<PanelSettings>(chemin);
            if (reglages == null)
            {
                reglages = ScriptableObject.CreateInstance<PanelSettings>();
                AssetDatabase.CreateAsset(reglages, chemin);
            }

            // Le thème fournit la police et les styles par défaut ; sans lui, les textes ne s’affichent pas
            reglages.themeStyleSheet = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>($"{Racine}/Interface/ThemeArene.tss");
            reglages.scaleMode = PanelScaleMode.ScaleWithScreenSize;   // l’interface garde ses proportions
            reglages.referenceResolution = new Vector2Int(1920, 1080);
            reglages.match = 0.5f;
            EditorUtility.SetDirty(reglages);
            return reglages;
        }

        private static Projectile CreerPrefabProjectile(Material materiau)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "Projectile";
            go.transform.localScale = Vector3.one * 0.3f;
            go.GetComponent<Renderer>().sharedMaterial = materiau;
            go.GetComponent<SphereCollider>().isTrigger = true;
            go.AddComponent<Rigidbody>().isKinematic = true;     // nécessaire pour recevoir OnTriggerEnter
            go.AddComponent<Projectile>();
            return EnregistrerPrefab(go, "Projectile").GetComponent<Projectile>();
        }

        private static Ennemi CreerPrefabEnnemi(Material materiauParDefaut)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Ennemi";
            go.GetComponent<Renderer>().sharedMaterial = materiauParDefaut;
            go.AddComponent<Rigidbody>().isKinematic = true;
            go.AddComponent<Ennemi>();
            return EnregistrerPrefab(go, "Ennemi").GetComponent<Ennemi>();
        }

        private static GameObject EnregistrerPrefab(GameObject go, string nom)
        {
            var prefab = PrefabUtility.SaveAsPrefabAsset(go, $"{Racine}/Prefabs/{nom}.prefab");
            Object.DestroyImmediate(go);
            return prefab;
        }
    }
}
