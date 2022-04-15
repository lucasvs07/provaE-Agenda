using provaE_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloPessoa
{
    public class Pessoa : EntidadeBase
    {
        public Pessoa(string nome, string email, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Empresa = empresa;
            Cargo = cargo;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }

        public override string ToString()
        {
            return "Id da Pessoa: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "E-mail: " + Email + Environment.NewLine +
                "Empresa: " + Empresa + Environment.NewLine +
                "Cargo: " + Cargo + Environment.NewLine;
        }
    }
}
