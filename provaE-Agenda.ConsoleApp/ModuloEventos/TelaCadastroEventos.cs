using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloContatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloEventos
{
    public class TelaCadastroEventos : TelaBase
    {
        private readonly RepositorioEventos repositorioEventos;
        private readonly IRepositorio<Contatos> repositorioContatos;
        private readonly TelaCadastroContatos telaCadastroContatos;
        private readonly Notificador notificador;

        public TelaCadastroEventos(RepositorioEventos repositorioEvento,
                RepositorioContatos repositorioContatos,
                TelaCadastroContatos telaCadastroContatos,
                Notificador notificador)
                : base("Cadastro de Evento")
        {
            this.repositorioEventos = repositorioEvento;
            this.notificador = notificador;
            this.repositorioContatos = repositorioContatos;
            this.telaCadastroContatos = telaCadastroContatos;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Eventos Diários");
            Console.WriteLine("Digite 6 para Visualizar Eventos Semanais");
            Console.WriteLine("Digite 7 para Visualizar Eventos Anteriores");
            Console.WriteLine("Digite 8 para Visualizar Eventos Futuros");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Evento");

            Eventos novoEvento = ObterEvento();
            if (novoEvento == null)
                return;

            repositorioEventos.Inserir(novoEvento);

            notificador.ApresentarMensagem("Evento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Evento");

            bool temEventosCadastrados = VisualizarRegistros("Pesquisando");

            if (temEventosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum evento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroEvento = ObterNumeroRegistro();

            Eventos eventoAtualizado = ObterEvento();

            bool conseguiuEditar = repositorioEventos.Editar(numeroEvento, eventoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Evento editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Evento");

            bool temEventosRegistrados = VisualizarRegistros("Pesquisando");

            if (temEventosRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum evento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroEvento = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioEventos.Excluir(numeroEvento);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Evento excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Eventos");

            List<Eventos> Eventos = repositorioEventos.SelecionarTodos();

            if (Eventos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum evento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Eventos evento in Eventos)
                Console.WriteLine(evento.ToString());

            Console.ReadLine();

            return true;
        }

        private Eventos ObterEvento()
        {
            Console.Write("Digite o assunto do evento: ");
            string assunto = Console.ReadLine();
            assunto = ValidaAssunto(assunto);

            Console.Write("Digite o local do evento: ");
            string local = Console.ReadLine();
            local = ValidaLocal(local);

            Console.Write("Digite a data do evento: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a hora de inicio do evento: ");
            DateTime horaInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a hora de término do evento: ");
            DateTime horaTermino = DateTime.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Selecione um contato para relacionar a este evento");
            Console.WriteLine();

            Contatos contatoSelecionado = ObtemContato();
            if (contatoSelecionado == null)
                return null;

            return new Eventos(assunto, local, data, horaInicio, horaTermino, contatoSelecionado);
        }

        private static string ValidaLocal(string local)
        {
            while (local.Length <= 0)
            {
                Console.Write("Digite o local do evento: ");
                local = Console.ReadLine();
            }

            return local;
        }

        private static string ValidaAssunto(string assunto)
        {
            while (assunto.Length <= 0)
            {
                Console.Write("Digite o assunto do evento: ");
                assunto = Console.ReadLine();
            }

            return assunto;
        }

        private Contatos ObtemContato()
        {
            bool temContatoDisponivel = telaCadastroContatos.VisualizarRegistros("Pesquisando");

            if (!temContatoDisponivel)
            {
                notificador.ApresentarMensagem("Não há nenhum contato cadastrado.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do contato: ");
            int idContato = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contatos contatoSelecionado = repositorioContatos.SelecionarRegistro(idContato);

            return contatoSelecionado;
        }

        public bool VisualizarEventosDiarios(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Eventos Diários");

            List<Eventos> eventosDiarios = repositorioEventos.SelecionarTodos();

            if (eventosDiarios.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum evento disponível para visualizar.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            foreach (Eventos evento in eventosDiarios)
                if (evento.Data.Day == data.Day && evento.Data.Month == data.Month && evento.Data.Year == data.Year)
                    Console.WriteLine(evento.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarEventosSemanais(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Eventos Semanais");

            List<Eventos> eventosSemanais = repositorioEventos.SelecionarTodos();

            if (eventosSemanais.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum evento disponível para visualização.", TipoMensagem.Atencao);
                return false;
            }

            DiaDaSemana dia = (DiaDaSemana)DateTime.Now.DayOfWeek;

            foreach (Eventos evento in eventosSemanais)
                if ((DiaDaSemana)evento.Data.DayOfWeek >= dia)
                    Console.WriteLine(evento.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarEventosAnteriores(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Eventos Anteriores");

            List<Eventos> eventosAnteriores = repositorioEventos.SelecionarTodos();

            if (eventosAnteriores.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum evento disponível.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            foreach (Eventos evento in eventosAnteriores)
                if (evento.Data < data)
                    Console.WriteLine(evento.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarEventosFuturos(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Eventos Futuro");

            List<Eventos> eventosFuturos = repositorioEventos.SelecionarTodos();

            if (eventosFuturos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum evento disponível para visualizar.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            Console.Write("Digite o mês: ");
            int mes = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (mes < 0 || mes > 12)
            {
                Console.Write("Mês inválido. Digite o mês: ");
                mes = int.Parse(Console.ReadLine());
            }

            foreach (Eventos evento in eventosFuturos)
                if (evento.Data > data && evento.Data.Month == mes)
                    Console.WriteLine(evento.ToString());

            Console.ReadLine();
            return true;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do evento: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioEventos.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do evento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
