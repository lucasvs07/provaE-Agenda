using provaE_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloItens
{
    public class TelaCadastroItens : TelaBase, ITelaCadastro
    {
        private readonly RepositorioItens repositorioItens;
        private readonly Notificador notificador;

        public TelaCadastroItens(RepositorioItens repositorioItens, Notificador notificador)
            : base("Cadastro de Item")
        {
            this.repositorioItens = repositorioItens;
            this.notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Item");

            Itens novoItem = ObterItem();

            repositorioItens.Inserir(novoItem);

            notificador.ApresentarMensagem("Item cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Item");

            bool temItensCadastrados = VisualizarRegistros("Pesquisando");

            if (temItensCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum item cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroItem = ObterNumeroRegistro();

            Itens ItemAtualizado = ObterItem();

            bool conseguiuEditar = repositorioItens.Editar(numeroItem, ItemAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Item editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Item");

            bool temItensRegistrados = VisualizarRegistros("Pesquisando");

            if (temItensRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum item cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroItem = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioItens.Excluir(numeroItem);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Item excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Item");

            List<Itens> itens = repositorioItens.SelecionarTodos();

            if (itens.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum item disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Itens item in itens)
                Console.WriteLine(item.ToString());

            Console.ReadLine();

            return true;
        }

        public Itens ObterItem()
        {
            Console.Write("Digite a descrição do item: ");
            string descricao = Console.ReadLine();
            descricao = ValidaDescricaoItem(descricao);

            Console.Write("O item está concluido (S para SIM ou N para NÃO): ");
            char concluido = char.Parse(Console.ReadLine());
            concluido = ValidaItemConcluido(concluido);

            return new Itens(descricao, concluido);
        }

        private static char ValidaItemConcluido(char concluido)
        {
            while (concluido != 'S' && concluido != 'N')
            {
                Console.Write("O item está concluido (S para SIM ou N para NÃO): ");
                concluido = char.Parse(Console.ReadLine());
            }

            return concluido;
        }

        private static string ValidaDescricaoItem(string descricao)
        {
            while (descricao.Length <= 0)
            {
                Console.Write("Digite a descrição do item: ");
                descricao = Console.ReadLine();
            }

            return descricao;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do item: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioItens.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do item não encontrado, tente novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
