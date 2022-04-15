using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.Compartilhado
{
    public interface ITelaCadastro
    {
        void Inserir();
        void Editar();
        void Excluir();
        bool VisualizarRegistros(string tipoVisualizacao);
    }
}
