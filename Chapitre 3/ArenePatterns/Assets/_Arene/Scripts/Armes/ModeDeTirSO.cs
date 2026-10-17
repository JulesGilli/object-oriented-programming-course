using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Une façon de tirer (pattern Strategy). Chaque stratégie est un asset :
    /// on crée et on règle de nouveaux modes de tir dans l’éditeur, sans toucher au code du joueur.
    /// </summary>
    public abstract class ModeDeTirSO : ScriptableObject
    {
        [SerializeField] private string nomAffiche = "Tir";
        [SerializeField, Min(0.05f)] private float cadence = 0.25f;   // secondes entre deux tirs
        [SerializeField, Min(1)] private int degats = 10;

        public string NomAffiche => nomAffiche;
        public float Cadence => cadence;
        protected int Degats => degats;

        public abstract void Tirer(PoolProjectiles projectiles, Transform canon);
    }
}
