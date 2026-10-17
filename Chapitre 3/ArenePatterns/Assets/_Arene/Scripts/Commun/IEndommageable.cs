namespace ArenePatterns
{
    /// <summary>Tout ce qui peut subir des dégâts : le joueur comme les ennemis.</summary>
    public interface IEndommageable
    {
        bool EstVivant { get; }
        void SubirDegats(int degats);
    }
}
