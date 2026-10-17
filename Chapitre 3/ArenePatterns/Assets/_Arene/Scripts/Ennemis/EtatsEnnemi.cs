using UnityEngine;

namespace ArenePatterns
{
    public interface IEtatEnnemi
    {
        bool EstVulnerable { get; }
        void Entrer(Ennemi ennemi);
        void Executer(Ennemi ennemi);
        void Sortir(Ennemi ennemi);
    }

    /// <summary>L’ennemi grandit pendant une courte durée, sans pouvoir être touché.</summary>
    public class EtatApparition : IEtatEnnemi
    {
        private const float Duree = 0.6f;
        private float debut;
        private Vector3 tailleFinale;

        public bool EstVulnerable => false;

        public void Entrer(Ennemi ennemi)
        {
            debut = Time.time;
            tailleFinale = ennemi.transform.localScale;
            ennemi.transform.localScale = Vector3.zero;
        }

        public void Executer(Ennemi ennemi)
        {
            float progression = (Time.time - debut) / Duree;
            ennemi.transform.localScale = Vector3.Lerp(Vector3.zero, tailleFinale, progression);

            if (progression >= 1f)
                ennemi.ChangerEtat(new EtatPoursuite());
        }

        public void Sortir(Ennemi ennemi) => ennemi.transform.localScale = tailleFinale;
    }

    /// <summary>L’ennemi avance vers sa cible, puis attaque quand il est assez près.</summary>
    public class EtatPoursuite : IEtatEnnemi
    {
        public bool EstVulnerable => true;

        public void Entrer(Ennemi ennemi) { }

        public void Executer(Ennemi ennemi)
        {
            Vector3 versCible = ennemi.Cible.position - ennemi.transform.position;
            versCible.y = 0f;

            if (versCible.magnitude <= ennemi.PorteeAttaque)
            {
                ennemi.ChangerEtat(new EtatAttaque());
                return;
            }

            ennemi.transform.position += versCible.normalized * (ennemi.Donnees.Vitesse * Time.deltaTime);
            ennemi.transform.rotation = Quaternion.LookRotation(versCible);
        }

        public void Sortir(Ennemi ennemi) { }
    }

    /// <summary>L’ennemi frappe à intervalle régulier ; si la cible s’éloigne, il reprend la poursuite.</summary>
    public class EtatAttaque : IEtatEnnemi
    {
        private float prochaineAttaque;

        public bool EstVulnerable => true;

        public void Entrer(Ennemi ennemi) => prochaineAttaque = Time.time;

        public void Executer(Ennemi ennemi)
        {
            Vector3 versCible = ennemi.Cible.position - ennemi.transform.position;
            versCible.y = 0f;

            if (versCible.magnitude > ennemi.PorteeAttaque * 1.2f)
            {
                ennemi.ChangerEtat(new EtatPoursuite());
                return;
            }

            if (Time.time >= prochaineAttaque && ennemi.CibleEndommageable.EstVivant)
            {
                ennemi.CibleEndommageable.SubirDegats(ennemi.Donnees.DegatsContact);
                prochaineAttaque = Time.time + ennemi.Donnees.IntervalleAttaque;
            }
        }

        public void Sortir(Ennemi ennemi) { }
    }

    /// <summary>État final : l’ennemi rétrécit puis disparaît. Aucune transition n’en sort.</summary>
    public class EtatMort : IEtatEnnemi
    {
        private const float Duree = 0.3f;
        private float debut;
        private Vector3 tailleInitiale;

        public bool EstVulnerable => false;

        public void Entrer(Ennemi ennemi)
        {
            debut = Time.time;
            tailleInitiale = ennemi.transform.localScale;
        }

        public void Executer(Ennemi ennemi)
        {
            float progression = (Time.time - debut) / Duree;
            ennemi.transform.localScale = Vector3.Lerp(tailleInitiale, Vector3.zero, progression);

            if (progression >= 1f)
                Object.Destroy(ennemi.gameObject);
        }

        public void Sortir(Ennemi ennemi) { }
    }
}
