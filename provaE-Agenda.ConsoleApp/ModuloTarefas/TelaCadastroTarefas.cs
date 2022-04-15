using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloTarefas
{
    public class TelaCadastroTarefas : TelaBase
    {
        private readonly RepositorioTarefas repositorioTarefas;
        private readonly Notificador notificador;
        private readonly List<Itens> itens = new List<Itens>();

        public TelaCadastroTarefas(RepositorioTarefas repositorioTarefas,
                Notificador notificador)
            : base("Cadastro de Tarefa")
        {
            this.repositorioTarefas = repositorioTarefas;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar Todas");
            Console.WriteLine("Digite 5 para Visualizar Pendentes");
            Console.WriteLine("Digite 6 para Visualizar Concluídas");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefa");

            Tarefas novaTarefa = ObterTarefa();

            if (novaTarefa == null)
                return;

            repositorioTarefas.Inserir(novaTarefa);

            notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            Tarefas tarefaAtualizada = ObterTarefaEditada(numeroTarefa);
            if (tarefaAtualizada == null)
                return;

            bool conseguiuEditar = repositorioTarefas.Editar(numeroTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasRegistradas = VisualizarRegistros("Pesquisando");

            if (temTarefasRegistradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioTarefas.Excluir(numeroTarefa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public void TarefaConcluida(int numeroTarefa)
        {
            repositorioTarefas.Excluir(numeroTarefa);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa");

            List<Tarefas> tarefas = repositorioTarefas.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefas tarefa in tarefas)
                if (tarefa.Prioridade == Prioridade.Alta)
                {
                    Console.WriteLine(tarefa.ToString());
                }

            foreach (Tarefas tarefa in tarefas)
                if (tarefa.Prioridade == Prioridade.Media)
                    Console.WriteLine(tarefa.ToString());

            foreach (Tarefas tarefa in tarefas)
                if (tarefa.Prioridade == Prioridade.Baixa)
                    Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTarefasPendentes(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Tarefas> tarefasPendentes = repositorioTarefas.SelecionarTodos();

            if (tarefasPendentes.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefas tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == Prioridade.Alta)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefas tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == Prioridade.Media)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefas tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == Prioridade.Baixa)
                    Console.WriteLine(tarefaPendente.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTarefasConcluidas(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Concluída");

            List<Tarefas> tarefasConcluidas = repositorioTarefas.SelecionarTodos();

            if (tarefasConcluidas.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefas tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == Prioridade.Alta)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefas tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == Prioridade.Media)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefas tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == Prioridade.Baixa)
                    Console.WriteLine(tarefaConcluida.ToString());

            Console.ReadLine();

            return true;
        }

        private Prioridade ConvertePrioridade(string prioridadeRecebida)
        {
            if (prioridadeRecebida.ToUpper().Equals("ALTA"))
            {
                return Prioridade.Alta;
            }
            else if (prioridadeRecebida.ToUpper().Equals("MEDIA"))
            {
                return Prioridade.Media;
            }

            return Prioridade.Baixa;
        }

        private Tarefas ObterTarefa()
        {
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();
            titulo = ValidaTituloVazio(titulo);

            Console.Write("Digite a prioridade da tarefa (Alta, Media ou Baixa): ");
            string stringPrioridade = Console.ReadLine();
            stringPrioridade = ValidaPrioridade(stringPrioridade);
            Prioridade prioridade = ConvertePrioridade(stringPrioridade);

            Console.Write("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a data de conclusão, ou de previsão de conclusão da tarefa: ");
            DateTime dataConclusao = DateTime.Parse(Console.ReadLine());

            char adicionarItens;
            List<Itens> itens = new List<Itens>();

            do
            {
                RepositorioItens repositorioItens = new RepositorioItens();
                TelaCadastroItens telaCadastroItens = new TelaCadastroItens(repositorioItens, notificador);

                Itens item = telaCadastroItens.ObterItem();
                itens.Add(item);

                adicionarItens = ValidaAdicionarOutroItem();

            } while (adicionarItens == 'S');

            int countConcluidos = 0;

            foreach (Itens item in itens)
                if (item.Concluido == 'S')
                    countConcluidos++;

            int percentual = 100 * countConcluidos / itens.Count;

            if (percentual == 100)
                return null;

            return new Tarefas(prioridade, titulo, dataCriacao, dataConclusao, percentual, itens);

        }

        private static string ValidaTituloVazio(string titulo)
        {
            while (titulo.Length <= 0)
            {
                Console.Write("Digite o titulo da tarefa: ");
                titulo = Console.ReadLine();
            }

            return titulo;
        }

        private static string ValidaPrioridade(string stringPrioridade)
        {
            while (stringPrioridade.ToUpper() != "ALTA" && stringPrioridade.ToUpper() != "MEDIA" && stringPrioridade.ToUpper() != "BAIXA")
            {
                Console.Write("Digite a prioridade da tarefa (Alta, Media ou Baixa): ");
                stringPrioridade = Console.ReadLine();
            }

            return stringPrioridade;
        }

        private Tarefas ObterTarefaEditada(int numeroIdTarefa)
        {

            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a prioridade da tarefa (Alta, Media ou Baixa): ");
            string stringPrioridade = Console.ReadLine();
            stringPrioridade = ValidaPrioridade(stringPrioridade);
            Prioridade prioridade = ConvertePrioridade(stringPrioridade);

            DateTime dataCriacao = repositorioTarefas.SelecionarRegistro(numeroIdTarefa).DataDeCriacao;

            DateTime dataConclusao = repositorioTarefas.SelecionarRegistro(numeroIdTarefa).DataDeConclusao;

            char adicionarItens;

            do
            {
                RepositorioItens repositorioItens = new RepositorioItens();
                TelaCadastroItens telaCadastroItens = new TelaCadastroItens(repositorioItens, notificador);

                Itens item = telaCadastroItens.ObterItem();
                itens.Add(item);

                adicionarItens = ValidaAdicionarOutroItem();

            } while (adicionarItens == 'S');

            int countConcluidos = 0;

            foreach (Itens item in itens)
                if (item.Concluido == 'S')
                    countConcluidos++;

            int percentual = 100 * countConcluidos / itens.Count;

            if (percentual == 100)
            {
                TarefaConcluida(numeroIdTarefa);
            }

            return new Tarefas(prioridade, titulo, dataCriacao, dataConclusao, percentual, itens);

        }

        private static char ValidaAdicionarOutroItem()
        {
            char adicionarItens;
            do
            {
                Console.Write("Adicionar outro item a esta tarefa (S para Sim ou N para Não): ");
                adicionarItens = char.Parse(Console.ReadLine());

            } while (adicionarItens != 'S' && adicionarItens != 'N');
            return adicionarItens;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da tarefa: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioTarefas.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da tarefa não foi encontrado, tente novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
