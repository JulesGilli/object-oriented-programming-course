using System;
using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Le contexte de la machine à états (pattern State). Il délègue son comportement à son état courant ;
    /// ce sont les états qui décident des transitions, sauf la mort, qui peut survenir dans n’importe quel état.
    /// </summary>
    public class Ennemi : MonoBehaviour, IEndommageable
    {
        private DonneesEnnemiSO donnees;
        private IEtatEnnemi etat;
        private int pv;

        public DonneesEnnemiSO Donnees => donnees;
        public Transform Cible { get; private set; }
        public IEndommageable CibleEndommageable { get; private set; }
        public float PorteeAttaque => 0.7f + donnees.Taille * 0.5f;
        public bool EstVivant => pv > 0;
        public string NomEtat => etat?.GetType().Name ?? "Aucun";

        public event Action<Ennemi> Elimine;

        public void Initialiser(DonneesEnnemiSO donnees, Transform cible)
        {
            this.donnees = donnees;
            Cible = cible;
            CibleEndommageable = cible.GetComponent<IEndommageable>();
            pv = donnees.PvMax;

            transform.localScale = Vector3.one * donnees.Taille;
            GetComponent<Renderer>().sharedMaterial = donnees.Materiau;   // pas de copie de matériau

            ChangerEtat(new EtatApparition());
        }

        public void ChangerEtat(IEtatEnnemi nouvelEtat)
        {
            etat?.Sortir(this);
            etat = nouvelEtat;
            etat.Entrer(this);
        }

        private void Update()
        {
            etat?.Executer(this);
        }

        public void SubirDegats(int degats)
        {
            if (!EstVivant || !etat.EstVulnerable)
                return;

            pv = Mathf.Max(0, pv - degats);
            if (!EstVivant)
            {
                ChangerEtat(new EtatMort());
                Elimine?.Invoke(this);
            }
        }
    }
}
