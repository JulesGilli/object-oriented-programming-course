using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Fait apparaître les ennemis par vagues de plus en plus grandes. La boucle des vagues est une
    /// coroutine (pattern Iterator) : elle se met en pause entre deux apparitions, puis jusqu’à ce que
    /// la vague soit vaincue.
    /// </summary>
    public class GenerateurVagues : MonoBehaviour
    {
        [SerializeField] private FabriqueEnnemis fabrique;
        [SerializeField] private DonneesEnnemiSO ennemiRapide;
        [SerializeField] private DonneesEnnemiSO ennemiCostaud;
        [SerializeField] private float rayonApparition = 13f;
        [SerializeField] private float delaiEntreApparitions = 0.4f;
        [SerializeField] private float pauseEntreVagues = 2f;

        private readonly List<Ennemi> vivants = new();
        private Coroutine boucle;

        public int NumeroVague { get; private set; }
        public int NombreVivants => vivants.Count;

        public event Action<int> VagueCommence;
        public event Action<Ennemi> EnnemiElimine;

        public void Demarrer()
        {
            Arreter();
            NumeroVague = 0;
            boucle = StartCoroutine(BoucleDesVagues());
        }

        public void Arreter()
        {
            if (boucle != null)
                StopCoroutine(boucle);
            boucle = null;

            foreach (Ennemi ennemi in vivants)
            {
                ennemi.Elimine -= SurElimination;
                Destroy(ennemi.gameObject);
            }
            vivants.Clear();
        }

        private IEnumerator BoucleDesVagues()
        {
            while (true)
            {
                NumeroVague++;
                VagueCommence?.Invoke(NumeroVague);

                foreach (DonneesEnnemiSO donnees in CompositionDeLaVague(NumeroVague))
                {
                    FaireApparaitre(donnees);
                    yield return new WaitForSeconds(delaiEntreApparitions);
                }

                yield return new WaitUntil(() => vivants.Count == 0);
                yield return new WaitForSeconds(pauseEntreVagues);
            }
        }

        // Vague n : 2 + 2n ennemis, dont un costaud sur trois
        public IEnumerable<DonneesEnnemiSO> CompositionDeLaVague(int numero)
        {
            int total = 2 + 2 * numero;
            for (int i = 1; i <= total; i++)
                yield return i % 3 == 0 ? ennemiCostaud : ennemiRapide;
        }

        public Ennemi FaireApparaitre(DonneesEnnemiSO donnees)
        {
            float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
            Vector3 position = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * rayonApparition;

            Ennemi ennemi = fabrique.Creer(donnees, position);
            ennemi.Elimine += SurElimination;
            vivants.Add(ennemi);
            return ennemi;
        }

        private void SurElimination(Ennemi ennemi)
        {
            ennemi.Elimine -= SurElimination;
            vivants.Remove(ennemi);
            EnnemiElimine?.Invoke(ennemi);
        }
    }
}
