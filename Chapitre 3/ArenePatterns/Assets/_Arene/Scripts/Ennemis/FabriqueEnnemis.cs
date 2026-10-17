using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Centralise la création des ennemis (pattern Factory). Le reste du jeu demande « un ennemi de tel type,
    /// à tel endroit » sans savoir comment il est assemblé : prefab, données, cible, état initial.
    /// </summary>
    public class FabriqueEnnemis : MonoBehaviour
    {
        [SerializeField] private Ennemi prefab;
        [SerializeField] private Transform cible;

        public Ennemi Creer(DonneesEnnemiSO donnees, Vector3 positionAuSol)
        {
            Vector3 position = positionAuSol + Vector3.up * (donnees.Taille / 2f);   // posé sur le sol
            Ennemi ennemi = Instantiate(prefab, position, Quaternion.identity, transform);
            ennemi.name = donnees.NomAffiche;
            ennemi.Initialiser(donnees, cible);
            return ennemi;
        }
    }
}
