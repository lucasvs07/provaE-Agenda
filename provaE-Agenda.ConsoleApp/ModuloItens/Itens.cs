using provaE_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloItens
{
    public class Itens : EntidadeBase
    {
        public Itens(string descricao, char concluido)
        {
            Descricao = descricao;
            Concluido = concluido;
        }

        public string Descricao { get; set; }
        public char Concluido { get; set; }

        public override string ToString()
        {
            return
                "Descrição: " + Descricao + Environment.NewLine +
                "Concluido: " + Concluido + Environment.NewLine;
        }
    }
}

