using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloEventos;
using provaE_Agenda.ConsoleApp.ModuloContatos;
using provaE_Agenda.ConsoleApp.ModuloTarefas;
using System;

namespace provaE_Agenda.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            TelaMenuPrincipal telaMenuPrincipal = new TelaMenuPrincipal(new Notificador());

            Notificador notificador = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastro)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroContatos)
                    GerenciarCadastroContato(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroTarefas)
                    GerenciarCadastroTarefa(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroEventos)
                    GerenciarCadastroCompromisso(telaSelecionada, opcaoSelecionada);

            }

            static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                ITelaCadastro telaCadastroBasico = telaSelecionada as ITelaCadastro;

                if (telaCadastroBasico is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroBasico.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroBasico.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroBasico.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroBasico.VisualizarRegistros("Tela");
            }

            static void GerenciarCadastroContato(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroContatos telaCadastroContato = telaSelecionada as TelaCadastroContatos;

                if (telaCadastroContato is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroContato.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroContato.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroContato.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroContato.VisualizarRegistros("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroContato.VisualizarContatosPorCargo("Tela");
            }

            static void GerenciarCadastroTarefa(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroTarefas telaCadastroTarefa = telaSelecionada as TelaCadastroTarefas;

                if (telaCadastroTarefa is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroTarefa.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroTarefa.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroTarefa.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroTarefa.VisualizarRegistros("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroTarefa.VisualizarTarefasPendentes("Tela");

                else if (opcaoSelecionada == "6")
                    telaCadastroTarefa.VisualizarTarefasConcluidas("Tela");
            }

            static void GerenciarCadastroCompromisso(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroEventos telaCadastroCompromisso = telaSelecionada as TelaCadastroEventos;

                if (telaCadastroCompromisso is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroCompromisso.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroCompromisso.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroCompromisso.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroCompromisso.VisualizarRegistros("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroCompromisso.VisualizarEventosDiarios("Tela");

                else if (opcaoSelecionada == "6")
                    telaCadastroCompromisso.VisualizarEventosSemanais("Tela");

                else if (opcaoSelecionada == "7")
                    telaCadastroCompromisso.VisualizarEventosAnteriores("Tela");

                else if (opcaoSelecionada == "8")
                    telaCadastroCompromisso.VisualizarEventosFuturos("Tela");
            }

        }
    }
}
