using UnityEngine;
using UnityEngine.UIElements;

namespace ArenePatterns
{
    /// <summary>
    /// L’interface de jeu, en UI Toolkit (pattern Observer). La structure est décrite dans Hud.uxml,
    /// l’apparence dans Hud.uss : ce script ne fait que relier les événements du jeu aux éléments
    /// de l’interface. Il ne lit jamais l’état des autres objets à chaque frame.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Joueur joueur;
        [SerializeField] private GestionnairePartie partie;
        [SerializeField] private GenerateurVagues generateur;
        [SerializeField, Range(0f, 1f)] private float seuilVieCritique = 0.3f;
        [SerializeField, Min(0.1f)] private float dureeAnnonceVague = 1.5f;

        private VisualElement remplissagePv;
        private VisualElement ecranFin;
        private Label textePv, texteArme, texteScore, texteVague, annonceVague, texteScoreFinal;
        private IVisualElementScheduledItem masquageAnnonce;

        private void OnEnable()
        {
            // UIDocument construit l’arbre visuel dans son propre OnEnable, exécuté avant celui-ci
            VisualElement racine = GetComponent<UIDocument>().rootVisualElement;
            remplissagePv = racine.Q<VisualElement>("barre-pv-remplissage");
            textePv = racine.Q<Label>("texte-pv");
            texteArme = racine.Q<Label>("texte-arme");
            texteScore = racine.Q<Label>("texte-score");
            texteVague = racine.Q<Label>("texte-vague");
            annonceVague = racine.Q<Label>("annonce-vague");
            ecranFin = racine.Q<VisualElement>("ecran-fin");
            texteScoreFinal = racine.Q<Label>("texte-score-final");

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

        private void SurPvChange(int pv, int pvMax)
        {
            float proportion = pvMax > 0 ? (float)pv / pvMax : 0f;

            textePv.text = $"{pv} / {pvMax}";
            remplissagePv.style.width = Length.Percent(proportion * 100f);       // animé par la transition USS
            remplissagePv.EnableInClassList("barre__remplissage--critique", proportion <= seuilVieCritique);
        }

        private void SurModeDeTirChange(ModeDeTirSO mode) => texteArme.text = mode.NomAffiche;

        private void SurScoreChange(int score) => texteScore.text = score.ToString();

        private void SurVagueCommence(int numero)
        {
            texteVague.text = $"Vague {numero}";
            annonceVague.text = $"Vague {numero}";
            annonceVague.AddToClassList("annonce--visible");

            // Masque l’annonce un peu plus tard ; une nouvelle vague annule le masquage précédent
            masquageAnnonce?.Pause();
            masquageAnnonce = annonceVague.schedule
                .Execute(() => annonceVague.RemoveFromClassList("annonce--visible"))
                .StartingIn((long)(dureeAnnonceVague * 1000));
        }

        private void SurPartieTerminee(int scoreFinal)
        {
            texteScoreFinal.text = $"Score : {scoreFinal}";
            ecranFin.AddToClassList("ecran-fin--visible");
        }

        private void SurPartieRelancee() => ecranFin.RemoveFromClassList("ecran-fin--visible");
    }
}
