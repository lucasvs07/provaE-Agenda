using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloPessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloContatos
{
    public class TelaCadastroContatos : TelaBase
    {
        private readonly IRepositorio<Contatos> repositorioContatos;
        private readonly IRepositorio<Pessoa> repositorioPessoa;
        private readonly TelaCadastroPessoa telaCadastroPessoa;
        private readonly Notificador notificador;

        public TelaCadastroContatos(RepositorioContatos repositorioContato,
            RepositorioPessoa repositorioPessoa,
            TelaCadastroPessoa telaCadastroPessoa,
            Notificador notificador)
            : base("Cadastro de Contato")
        {
            this.repositorioContatos = repositorioContato;
            this.repositorioPessoa = repositorioPessoa;
            this.telaCadastroPessoa = telaCadastroPessoa;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar por Cargo");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contato");

            Contatos novoContato = ObterContato();
            if (novoContato == null)
                return;

            repositorioContatos.Inserir(novoContato);

            notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            Contatos contatoAtualizado = ObterContato();
            if (contatoAtualizado == null)
                return;

            bool conseguiuEditar = repositorioContatos.Editar(numeroContato, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosRegistrados = VisualizarRegistros("Pesquisando");

            if (temContatosRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioContatos.Excluir(numeroContato);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contato");

            List<Contatos> contatos = repositorioContatos.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contatos contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarContatosPorCargo(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contato por Cargo");

            List<Contatos> contatos = repositorioContatos.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            Console.WriteLine("Cargos Cadastrados");
            Console.WriteLine();

            foreach (Contatos contato in contatos)
                Console.WriteLine(contato.Pessoa.Cargo);

            Console.WriteLine();

            Console.Write("Você deseja visualizar contatos que ocupam qual cargo: ");
            string cargoOcupado = Console.ReadLine();
            Console.WriteLine();

            foreach (Contatos contato in contatos)
                if (contato.Pessoa.Cargo == cargoOcupado)
                    Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        private Contatos ObterContato()
        {
            Pessoa pessoaSelecionada = ObtemPessoa();
            if (pessoaSelecionada == null)
                return null;

            Console.Write("Digite o número de telefone da pessoa: ");
            string telefone = Console.ReadLine();
            telefone = ValidaTelefone(telefone);

            return new Contatos(pessoaSelecionada, telefone);
        }

        private static string ValidaTelefone(string telefone)
        {
            while (telefone.Length <= 0)
            {
                Console.Write("Digite o número de telefone da pessoa: ");
                telefone = Console.ReadLine();
            }

            return telefone;
        }

        private Pessoa ObtemPessoa()
        {
            bool temPessoaDisponivel = telaCadastroPessoa.VisualizarRegistros("Pesquisando");

            if (!temPessoaDisponivel)
            {
                notificador.ApresentarMensagem("Não há nenhuma pessoa cadastrada.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id da pessoa: ");
            int idPessoa = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Pessoa pessoaSelecionada = repositorioPessoa.SelecionarRegistro(idPessoa);

            return pessoaSelecionada;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do contato: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioContatos.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do contato não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
