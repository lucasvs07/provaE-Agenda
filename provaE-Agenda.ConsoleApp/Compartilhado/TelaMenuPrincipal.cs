using provaE_Agenda.ConsoleApp.ModuloEventos;
using provaE_Agenda.ConsoleApp.ModuloContatos;
using provaE_Agenda.ConsoleApp.ModuloItens;
using provaE_Agenda.ConsoleApp.ModuloPessoa;
using provaE_Agenda.ConsoleApp.ModuloTarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private RepositorioPessoa repositorioPessoa;
        private TelaCadastroPessoa telaCadastroPessoa;

        private RepositorioContatos repositorioContatos;
        private TelaCadastroContatos telaCadastroContatos;

        private RepositorioTarefas repositorioTarefas;
        private TelaCadastroTarefas telaCadastroTarefas;

        private RepositorioItens repositorioItens;
        private TelaCadastroItens telaCadastroItens;

        private RepositorioEventos repositorioCompromissos;
        private TelaCadastroEventos telaCadastroCompromissos;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioPessoa = new RepositorioPessoa();
            telaCadastroPessoa = new TelaCadastroPessoa(repositorioPessoa, notificador);

            repositorioContatos = new RepositorioContatos();
            telaCadastroContatos = new TelaCadastroContatos(repositorioContatos, repositorioPessoa, telaCadastroPessoa, notificador);

            repositorioTarefas = new RepositorioTarefas();
            telaCadastroTarefas = new TelaCadastroTarefas(repositorioTarefas, notificador);

            repositorioItens = new RepositorioItens();
            telaCadastroItens = new TelaCadastroItens(repositorioItens, notificador);

            repositorioCompromissos = new RepositorioEventos();
            telaCadastroCompromissos = new TelaCadastroEventos(repositorioCompromissos, repositorioContatos, telaCadastroContatos, notificador);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("e-Agenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Pessoas");
            Console.WriteLine("Digite 2 para Gerenciar Contatos");
            Console.WriteLine("Digite 3 para Gerenciar Tarefas");
            Console.WriteLine("Digite 4 para Gerenciar Compromissos");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroPessoa;

            else if (opcao == "2")
                tela = telaCadastroContatos;

            else if (opcao == "3")
                tela = telaCadastroTarefas;

            else if (opcao == "4")
                tela = telaCadastroCompromissos;

            return tela;
        }
    }
}
