using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloPessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloContatos
{
    public class Contatos : EntidadeBase
    {
        public Contatos(Pessoa pessoa, string telefone)
        {
            Pessoa = pessoa;
            Telefone = telefone;
        }

        public Pessoa Pessoa { get; set; }
        public string Telefone { get; set; }

        public override string ToString()
        {
            return "Id do Contato: " + id + Environment.NewLine +
                "Nome da Pessoa: " + Pessoa.Nome + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine;
        }
    }
}
