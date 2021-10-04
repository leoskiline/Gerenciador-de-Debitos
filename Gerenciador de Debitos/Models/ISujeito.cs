namespace Gerenciador_de_Debitos.Models
{
    public interface ISujeito // Quem será observado...
    {
        void AdicionarOBS(IObservador observador);
        void RemoverOBS(IObservador observador);
        void NotificarOBS();
    }
}
