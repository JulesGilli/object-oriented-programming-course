using UnityEngine;
using UnityEngine.Pool;

namespace ArenePatterns
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float vitesse = 25f;
        [SerializeField] private float duree = 1.2f;

        private IObjectPool<Projectile> pool;
        private float tempsRestant;
        private int degats;
        private bool dejaRendu;

        public void DefinirPool(IObjectPool<Projectile> pool) => this.pool = pool;

        // Appelé à chaque sortie du pool : on remet TOUT l’état à zéro
        public void Initialiser(int degats)
        {
            this.degats = degats;
            tempsRestant = duree;
            dejaRendu = false;
        }

        private void Update()
        {
            transform.position += transform.forward * (vitesse * Time.deltaTime);

            tempsRestant -= Time.deltaTime;
            if (tempsRestant <= 0f)
                Rendre();
        }

        private void OnTriggerEnter(Collider autre)
        {
            if (dejaRendu)
                return;

            if (autre.TryGetComponent(out IEndommageable cible) && cible.EstVivant && cible is not Joueur)
            {
                cible.SubirDegats(degats);
                Rendre();
            }
        }

        private void Rendre()
        {
            if (dejaRendu)
                return;      // un projectile ne doit jamais être rendu deux fois au pool

            dejaRendu = true;
            pool.Release(this);
        }
    }
}
