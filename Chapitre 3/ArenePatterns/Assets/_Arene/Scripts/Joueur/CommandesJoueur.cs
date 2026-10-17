using UnityEngine;

namespace ArenePatterns
{
    /// <summary>
    /// Une action demandée au joueur, transformée en objet (pattern Command).
    /// Le clavier, une manette, une IA de démonstration ou un système de replay
    /// peuvent produire les mêmes commandes : le joueur ne voit pas la différence.
    /// </summary>
    public interface ICommandeJoueur
    {
        void Executer(Joueur joueur);
    }

    public class CommandeDeplacement : ICommandeJoueur
    {
        private readonly Vector2 direction;
        public CommandeDeplacement(Vector2 direction) => this.direction = direction;
        public void Executer(Joueur joueur) => joueur.Deplacer(direction);
    }

    public class CommandeVisee : ICommandeJoueur
    {
        private readonly Vector3 pointVise;
        public CommandeVisee(Vector3 pointVise) => this.pointVise = pointVise;
        public void Executer(Joueur joueur) => joueur.Viser(pointVise);
    }

    public class CommandeTir : ICommandeJoueur
    {
        public void Executer(Joueur joueur) => joueur.Tirer();
    }

    public class CommandeChangerModeDeTir : ICommandeJoueur
    {
        private readonly int index;
        public CommandeChangerModeDeTir(int index) => this.index = index;
        public void Executer(Joueur joueur) => joueur.ChoisirModeDeTir(index);
    }
}
