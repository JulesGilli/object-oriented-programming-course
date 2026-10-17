using UnityEngine;

namespace ArenePatterns
{
    [CreateAssetMenu(menuName = "Arène/Mode de tir/Tir en éventail")]
    public class TirEnEventailSO : ModeDeTirSO
    {
        [SerializeField, Min(2)] private int nombreDeProjectiles = 5;
        [SerializeField, Range(5f, 180f)] private float angleTotal = 50f;

        public override void Tirer(PoolProjectiles projectiles, Transform canon)
        {
            for (int i = 0; i < nombreDeProjectiles; i++)
            {
                float angle = -angleTotal / 2f + angleTotal * i / (nombreDeProjectiles - 1);
                projectiles.Lancer(canon.position, canon.rotation * Quaternion.Euler(0f, angle, 0f), Degats);
            }
        }
    }
}
