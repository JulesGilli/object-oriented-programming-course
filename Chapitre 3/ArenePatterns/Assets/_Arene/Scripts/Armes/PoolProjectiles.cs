using UnityEngine;
using UnityEngine.Pool;

namespace ArenePatterns
{
    /// <summary>Recycle les projectiles au lieu de les créer et de les détruire (pattern Object Pool).</summary>
    public class PoolProjectiles : MonoBehaviour
    {
        [SerializeField] private Projectile prefab;
        [SerializeField] private int capaciteInitiale = 30;
        [SerializeField] private int tailleMax = 200;

        private ObjectPool<Projectile> pool;

        public int NombreCrees { get; private set; }
        public int NombreActifs => pool.CountActive;

        private void Awake()
        {
            pool = new ObjectPool<Projectile>(
                createFunc: Creer,
                actionOnGet: projectile => projectile.gameObject.SetActive(true),
                actionOnRelease: projectile => projectile.gameObject.SetActive(false),
                actionOnDestroy: projectile => Destroy(projectile.gameObject),
                collectionCheck: true,
                defaultCapacity: capaciteInitiale,
                maxSize: tailleMax);
        }

        private Projectile Creer()
        {
            NombreCrees++;
            Projectile projectile = Instantiate(prefab, transform);
            projectile.DefinirPool(pool);
            return projectile;
        }

        public void Lancer(Vector3 position, Quaternion rotation, int degats)
        {
            Projectile projectile = pool.Get();
            projectile.transform.SetPositionAndRotation(position, rotation);
            projectile.Initialiser(degats);
        }
    }
}
