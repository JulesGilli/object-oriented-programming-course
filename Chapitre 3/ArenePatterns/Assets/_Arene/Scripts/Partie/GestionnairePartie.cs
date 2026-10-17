using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArenePatterns
{
    /// <summary>
    /// Orchestre la partie : démarrage, score, fin de partie et relance.
    /// Il réagit aux événements du joueur et du générateur, et en publie à son tour pour le HUD.
    /// </summary>
    public class GestionnairePartie : MonoBehaviour
    {
        [SerializeField] private Joueur joueur;
        [SerializeField] private GenerateurVagues generateur;

        private InputAction relancer;

        public int Score { get; private set; }
        public bool EstTerminee { get; private set; }

        public event Action<int> ScoreChange;
        public event Action<int> PartieTerminee;       // score final
        public event Action PartieRelancee;

        private void Awake()
        {
            relancer = new InputAction("Relancer", InputActionType.Button, "<Keyboard>/r");
            relancer.AddBinding("<Gamepad>/start");
        }

        private void OnEnable()
        {
            relancer.Enable();
            joueur.Mort += SurMortDuJoueur;
            generateur.EnnemiElimine += SurEnnemiElimine;
        }

        private void OnDisable()
        {
            relancer.Disable();
            joueur.Mort -= SurMortDuJoueur;
            generateur.EnnemiElimine -= SurEnnemiElimine;
        }

        private void OnDestroy() => relancer.Dispose();

        private void Start() => NouvellePartie();

        private void Update()
        {
            if (EstTerminee && relancer.WasPressedThisFrame())
                NouvellePartie();
        }

        public void NouvellePartie()
        {
            EstTerminee = false;
            Score = 0;
            ScoreChange?.Invoke(Score);

            joueur.Reinitialiser();
            generateur.Demarrer();
            PartieRelancee?.Invoke();
        }

        private void SurEnnemiElimine(Ennemi ennemi)
        {
            if (EstTerminee)
                return;

            Score += ennemi.Donnees.Points;
            ScoreChange?.Invoke(Score);
        }

        private void SurMortDuJoueur()
        {
            EstTerminee = true;
            generateur.Arreter();
            PartieTerminee?.Invoke(Score);
        }
    }
}
