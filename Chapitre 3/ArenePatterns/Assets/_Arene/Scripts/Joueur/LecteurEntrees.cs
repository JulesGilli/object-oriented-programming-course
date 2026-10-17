using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArenePatterns
{
    /// <summary>
    /// Lit les entrées (Input System) et les traduit en commandes pour le joueur.
    /// C’est le seul script du jeu qui connaît le clavier, la souris et la manette.
    /// </summary>
    public class LecteurEntrees : MonoBehaviour
    {
        [SerializeField] private Joueur joueur;
        [SerializeField] private Camera cameraJeu;

        private InputAction deplacement;
        private InputAction tir;
        private InputAction modeDeTir1;
        private InputAction modeDeTir2;
        private readonly Plane sol = new Plane(Vector3.up, Vector3.zero);

        private void Awake()
        {
            // Les chemins désignent la POSITION physique des touches :
            // « w » est la touche Z sur un clavier AZERTY.
            deplacement = new InputAction("Déplacement", InputActionType.Value);
            deplacement.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            deplacement.AddBinding("<Gamepad>/leftStick");

            tir = new InputAction("Tir", InputActionType.Button, "<Mouse>/leftButton");
            tir.AddBinding("<Keyboard>/space");
            tir.AddBinding("<Gamepad>/rightTrigger");

            modeDeTir1 = new InputAction("Mode de tir 1", InputActionType.Button, "<Keyboard>/digit1");
            modeDeTir2 = new InputAction("Mode de tir 2", InputActionType.Button, "<Keyboard>/digit2");
        }

        private void OnEnable()
        {
            deplacement.Enable();
            tir.Enable();
            modeDeTir1.Enable();
            modeDeTir2.Enable();
        }

        private void OnDisable()
        {
            deplacement.Disable();
            tir.Disable();
            modeDeTir1.Disable();
            modeDeTir2.Disable();
        }

        private void OnDestroy()
        {
            deplacement.Dispose();
            tir.Dispose();
            modeDeTir1.Dispose();
            modeDeTir2.Dispose();
        }

        private void Update()
        {
            foreach (ICommandeJoueur commande in LireCommandes())
                commande.Executer(joueur);
        }

        // Pattern Iterator : les commandes de la frame sont produites à la demande
        private IEnumerable<ICommandeJoueur> LireCommandes()
        {
            Vector2 direction = deplacement.ReadValue<Vector2>();
            if (direction != Vector2.zero)
                yield return new CommandeDeplacement(direction);

            if (Mouse.current != null && TrouverPointVise(Mouse.current.position.ReadValue(), out Vector3 point))
                yield return new CommandeVisee(point);

            if (tir.IsPressed())
                yield return new CommandeTir();

            if (modeDeTir1.WasPressedThisFrame())
                yield return new CommandeChangerModeDeTir(0);
            if (modeDeTir2.WasPressedThisFrame())
                yield return new CommandeChangerModeDeTir(1);
        }

        private bool TrouverPointVise(Vector2 positionEcran, out Vector3 point)
        {
            Ray rayon = cameraJeu.ScreenPointToRay(positionEcran);
            if (sol.Raycast(rayon, out float distance))
            {
                point = rayon.GetPoint(distance);
                return true;
            }

            point = default;
            return false;
        }
    }
}
