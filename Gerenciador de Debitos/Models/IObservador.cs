using System;

namespace Gerenciador_de_Debitos.Models
{
    public interface IObservador // Quem observa...
    {
        void Update(string acao, params Object[] parametros);
    }
}
