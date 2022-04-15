using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloTarefas
{
    public class Tarefas : EntidadeBase
    {
        public Tarefas(Prioridade prioridade, string titulo,
         DateTime dataDeCriacao, DateTime dataDeConclusao, int percentualConcluido, List<Itens> itens)
        {
            Prioridade = prioridade;
            Titulo = titulo;
            DataDeCriacao = dataDeCriacao;
            DataDeConclusao = dataDeConclusao;
            PercentualConcluido = percentualConcluido;
            Itens = itens;
        }

        public Prioridade Prioridade { get; set; }
        public string Titulo { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public DateTime DataDeConclusao { get; set; }
        public int PercentualConcluido { get; set; }

        public List<Itens> Itens;
        public override string ToString()
        {
            return "Id da Tarefa: " + id + Environment.NewLine +
                "Titulo: " + Titulo + Environment.NewLine +
                "Prioridade: " + Prioridade + Environment.NewLine +
                "Data de Criação: " + DataDeCriacao + Environment.NewLine +
                "Data de Conclusão: " + DataDeConclusao + Environment.NewLine +
                "Percentual Concluído: " + PercentualConcluido + "%" + Environment.NewLine +
                "\nItens da Tarefa: \n" + Environment.NewLine +
                ListarItensTarefa() + "\n_____________________________________\n";
        }
        public string ListarItensTarefa()
        {
            string itensString = "";

            foreach (Itens item in Itens)
                itensString += item.ToString() + "\n";

            return itensString;
        }
    }
}
