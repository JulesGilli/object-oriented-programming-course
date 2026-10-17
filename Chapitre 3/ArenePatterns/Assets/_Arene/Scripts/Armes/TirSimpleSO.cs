using UnityEngine;

namespace ArenePatterns
{
    [CreateAssetMenu(menuName = "Arène/Mode de tir/Tir simple")]
    public class TirSimpleSO : ModeDeTirSO
    {
        public override void Tirer(PoolProjectiles projectiles, Transform canon)
            => projectiles.Lancer(canon.position, canon.rotation, Degats);
    }
}
