using provaE_Agenda.ConsoleApp.Compartilhado;
using provaE_Agenda.ConsoleApp.ModuloContatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.ModuloEventos
{
    public class Eventos : EntidadeBase
    {
        public Eventos(string assunto, string local, DateTime data,
            DateTime horaInicio, DateTime horaTermino, Contatos contato)
        {
            Assunto = assunto;
            Local = local;
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            Contato = contato;
        }

        public string Assunto { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraTermino { get; set; }
        public Contatos Contato { get; set; }

        public override string ToString()
        {
            return "Id do Evento: " + id + Environment.NewLine +
                "Assunto: " + Assunto + Environment.NewLine +
                "Local: " + Local + Environment.NewLine +
                "Data: " + Data.Day + "/" + Data.Month + "/" + Data.Year + Environment.NewLine +
                "Hora de inicio: " + HoraInicio.Hour + ":" + HoraInicio.Minute + Environment.NewLine +
                "Hora de termino: " + HoraTermino.Hour + ":" + HoraTermino.Minute + Environment.NewLine +
                    Contato.ToString() + Environment.NewLine;
        }
    }
}
