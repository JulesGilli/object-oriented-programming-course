using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Les caractéristiques d’un TYPE d’ennemi, partagées par tous les ennemis de ce type (pattern Flyweight).
    /// Ces données ne sont jamais modifiées en jeu : l’état propre à chaque ennemi (PV, position) vit dans Ennemi.
    /// </summary>
    [CreateAssetMenu(menuName = "Arène/Données d’ennemi")]
    public class DonneesEnnemiSO : ScriptableObject
    {
        [SerializeField] private string nomAffiche = "Ennemi";
        [SerializeField, Min(1)] private int pvMax = 30;
        [SerializeField, Min(0f)] private float vitesse = 3f;
        [SerializeField, Min(0)] private int degatsContact = 10;
        [SerializeField, Min(0.1f)] private float intervalleAttaque = 1f;
        [SerializeField, Min(0)] private int points = 10;
        [SerializeField, Min(0.1f)] private float taille = 1f;
        [SerializeField] private Material materiau;     // partagé : on utilise sharedMaterial, jamais material

        public string NomAffiche => nomAffiche;
        public int PvMax => pvMax;
        public float Vitesse => vitesse;
        public int DegatsContact => degatsContact;
        public float IntervalleAttaque => intervalleAttaque;
        public int Points => points;
        public float Taille => taille;
        public Material Materiau => materiau;
    }
}
