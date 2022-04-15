using provaE_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloPessoa
{
    public class TelaCadastroPessoa : TelaBase
    {
        private readonly RepositorioPessoa repositorioPessoa;
        private readonly Notificador notificador;

        public TelaCadastroPessoa(RepositorioPessoa repositorioPessoa, Notificador notificador)
            : base("Cadastro de Pessoa")
        {
            this.repositorioPessoa = repositorioPessoa;
            this.notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Pessoa");

            Pessoa novoPessoa = ObterPessoa();

            repositorioPessoa.Inserir(novoPessoa);

            notificador.ApresentarMensagem("Pessoa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Pessoa");

            bool temPessoasCadastradas = VisualizarRegistros("Pesquisando");

            if (temPessoasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma pessoa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPessoa = ObterNumeroRegistro();

            Pessoa pessoaAtualizada = ObterPessoa();

            bool conseguiuEditar = repositorioPessoa.Editar(numeroPessoa, pessoaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Pessoa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Pessoa");

            bool temPessoasRegistradas = VisualizarRegistros("Pesquisando");

            if (temPessoasRegistradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma pessoa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPessoa = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioPessoa.Excluir(numeroPessoa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Pessoa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Pessoa");

            List<Pessoa> pessoas = repositorioPessoa.SelecionarTodos();

            if (pessoas.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma pessoa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Pessoa pessoa in pessoas)
                Console.WriteLine(pessoa.ToString());

            Console.ReadLine();

            return true;
        }

        private Pessoa ObterPessoa()
        {
            Console.Write("Digite o nome da pessoa: ");
            string nome = Console.ReadLine();
            nome = ValidaNome(nome);

            Console.Write("Digite o e-mail da pessoa: ");
            string email = Console.ReadLine();
            email = ValidaEmail(email);

            Console.Write("Digite a empresa onde a pessoa trabalha: ");
            string empresa = Console.ReadLine();
            empresa = ValidaEmpresa(empresa);

            Console.Write("Digite o cargo que a pessoa ocupa: ");
            string cargo = Console.ReadLine();
            cargo = ValidaCargo(cargo);

            return new Pessoa(nome, email, empresa, cargo);
        }

        private static string ValidaCargo(string cargo)
        {
            while (cargo.Length <= 0)
            {
                Console.Write("Digite o cargo que a pessoa ocupa: ");
                cargo = Console.ReadLine();
            }

            return cargo;
        }

        private static string ValidaEmpresa(string empresa)
        {
            while (empresa.Length <= 0)
            {
                Console.Write("Digite a empresa onde a pessoa trabalha: ");
                empresa = Console.ReadLine();
            }

            return empresa;
        }

        private static string ValidaNome(string nome)
        {
            while (nome.Length <= 0)
            {
                Console.Write("Digite o nome da pessoa: ");
                nome = Console.ReadLine();
            }

            return nome;
        }

        private static string ValidaEmail(string email)
        {
            while (email.Length <= 0)
            {
                Console.Write("Digite o e-mail da pessoa: ");
                email = Console.ReadLine();
            }

            return email;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da pessoa: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioPessoa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da pessoa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
