using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{
    public abstract class ClasseInutil
    {
        public abstract bool DeletarPorID();
        public abstract bool Cadastrar();
        public abstract bool AlterarConta();
    }
}
