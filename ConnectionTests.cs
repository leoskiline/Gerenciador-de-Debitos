using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gerenciador_de_Debitos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Gerenciador_de_Debitos.Tests
{
    [TestClass()]
    public class ConnectionTests
    {
        [TestMethod()]
        public void getConnectionTest()
        {
            Connection Conexao = Connection.getConnection();
            DataTable dt =  Conexao.ExecuteSelect("SELECT * FROM usuario");
            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine(row["nome"]);
            }
            IEnumerable<DataRow> dr = dt.AsEnumerable();
            Array vetor = dr.ToArray();
            Assert.IsNotNull(dt);
        }
    }
}