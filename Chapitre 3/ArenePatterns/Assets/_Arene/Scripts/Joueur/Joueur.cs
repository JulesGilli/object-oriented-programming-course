using System;
using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Le joueur. Il ne lit jamais le clavier : il exécute des commandes (pattern Command)
    /// et prévient le reste du jeu par des événements (pattern Observer).
    /// </summary>
    public class Joueur : MonoBehaviour, IEndommageable
    {
        [SerializeField] private float vitesse = 7f;
        [SerializeField] private int pvMax = 100;
        [SerializeField] private float limiteArene = 14f;
        [SerializeField] private Transform canon;
        [SerializeField] private PoolProjectiles projectiles;
        [SerializeField] private ModeDeTirSO[] modesDeTir;

        private int pv;
        private int indexModeDeTir;
        private float prochainTir;

        public int Pv => pv;
        public int PvMax => pvMax;
        public bool EstVivant => pv > 0;
        public ModeDeTirSO ModeDeTir => modesDeTir[indexModeDeTir];

        public event Action<int, int> PvChange;              // (pv, pvMax)
        public event Action<ModeDeTirSO> ModeDeTirChange;
        public event Action Mort;

        private void Awake()
        {
            pv = pvMax;
        }

        private void Start()
        {
            // Les abonnés (HUD…) se sont inscrits dans leur OnEnable : on leur donne l’état initial
            PvChange?.Invoke(pv, pvMax);
            ModeDeTirChange?.Invoke(ModeDeTir);
        }

        public void Deplacer(Vector2 direction)
        {
            if (!EstVivant)
                return;

            Vector3 deplacement = new Vector3(direction.x, 0f, direction.y) * (vitesse * Time.deltaTime);
            Vector3 position = transform.position + deplacement;
            position.x = Mathf.Clamp(position.x, -limiteArene, limiteArene);
            position.z = Mathf.Clamp(position.z, -limiteArene, limiteArene);
            transform.position = position;
        }

        public void Viser(Vector3 pointVise)
        {
            if (!EstVivant)
                return;

            Vector3 direction = pointVise - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(direction);
        }

        public void Tirer()
        {
            if (!EstVivant || Time.time < prochainTir)
                return;

            prochainTir = Time.time + ModeDeTir.Cadence;
            ModeDeTir.Tirer(projectiles, canon);      // la stratégie de tir décide du reste
        }

        public void ChoisirModeDeTir(int index)
        {
            if (index < 0 || index >= modesDeTir.Length || index == indexModeDeTir)
                return;

            indexModeDeTir = index;
            ModeDeTirChange?.Invoke(ModeDeTir);
        }

        public void SubirDegats(int degats)
        {
            if (degats < 0)
                throw new ArgumentOutOfRangeException(nameof(degats), "Les dégâts ne peuvent pas être négatifs.");
            if (!EstVivant)
                return;

            pv = Mathf.Max(0, pv - degats);
            PvChange?.Invoke(pv, pvMax);

            if (!EstVivant)
                Mort?.Invoke();
        }

        public void Reinitialiser()
        {
            pv = pvMax;
            transform.SetPositionAndRotation(new Vector3(0f, transform.position.y, 0f), Quaternion.identity);
            PvChange?.Invoke(pv, pvMax);
        }
    }
}
