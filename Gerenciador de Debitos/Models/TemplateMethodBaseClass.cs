using Gerenciador_de_Debitos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{
    public abstract class TemplateMethodBaseClass: ClasseInutil
    {
       
       public override sealed void TemplateMethod()
        {
            this.Cadastrar();
            this.AlterarConta();
            this.DeletarPorID();
          
        }
      
        public abstract bool DeletarPorID();
        public void Cadastrar() { }
        public abstract bool AlterarConta();
     
       



    }
}
