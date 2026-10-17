using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// L’interface de jeu (pattern Observer) : elle ne lit jamais l’état des autres objets à chaque frame,
    /// elle mémorise ce que les événements lui apprennent. Elle utilise l’IMGUI (OnGUI) pour rester
    /// autonome ; un vrai jeu utiliserait uGUI ou UI Toolkit, avec exactement les mêmes abonnements.
    /// </summary>
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Joueur joueur;
        [SerializeField] private GestionnairePartie partie;
        [SerializeField] private GenerateurVagues generateur;

        private int pv, pvMax, score, vague;
        private string modeDeTir = "";
        private bool partieTerminee;
        private GUIStyle style, styleTitre;

        private void OnEnable()
        {
            joueur.PvChange += SurPvChange;
            joueur.ModeDeTirChange += SurModeDeTirChange;
            partie.ScoreChange += SurScoreChange;
            partie.PartieTerminee += SurPartieTerminee;
            partie.PartieRelancee += SurPartieRelancee;
            generateur.VagueCommence += SurVagueCommence;
        }

        private void OnDisable()
        {
            joueur.PvChange -= SurPvChange;
            joueur.ModeDeTirChange -= SurModeDeTirChange;
            partie.ScoreChange -= SurScoreChange;
            partie.PartieTerminee -= SurPartieTerminee;
            partie.PartieRelancee -= SurPartieRelancee;
            generateur.VagueCommence -= SurVagueCommence;
        }

        private void SurPvChange(int pv, int pvMax) { this.pv = pv; this.pvMax = pvMax; }
        private void SurModeDeTirChange(ModeDeTirSO mode) => modeDeTir = mode.NomAffiche;
        private void SurScoreChange(int score) => this.score = score;
        private void SurPartieTerminee(int scoreFinal) => partieTerminee = true;
        private void SurPartieRelancee() => partieTerminee = false;
        private void SurVagueCommence(int numero) => vague = numero;

        private void OnGUI()
        {
            style ??= new GUIStyle(GUI.skin.label) { fontSize = 22 };
            styleTitre ??= new GUIStyle(GUI.skin.label) { fontSize = 40, alignment = TextAnchor.MiddleCenter };

            GUI.Label(new Rect(20, 15, 400, 30), $"PV : {pv} / {pvMax}", style);
            GUI.Label(new Rect(20, 45, 400, 30), $"Score : {score}", style);
            GUI.Label(new Rect(20, 75, 400, 30), $"Vague : {vague}", style);
            GUI.Label(new Rect(20, 105, 600, 30), $"Arme : {modeDeTir}   (touches 1 et 2)", style);

            if (partieTerminee)
                GUI.Label(new Rect(0, Screen.height / 2f - 60, Screen.width, 120),
                          $"Partie terminée — score : {score}\nAppuyez sur R pour rejouer", styleTitre);
        }
    }
}
