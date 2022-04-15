﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaE_Agenda.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        bool Editar(int idSelecionado, T novaEntidade);
        bool Editar(Predicate<T> condicao, T novaEntidade);
        bool Excluir(int idSelecionado);
        bool Excluir(Predicate<T> condicao);
        bool ExisteRegistro(int idSelecionado);
        bool ExisteRegistro(Predicate<T> condicao);
        T SelecionarRegistro(int idSelecionado);
        T SelecionarRegistro(Predicate<T> condicao);
        List<T> Filtrar(Predicate<T> condicao);
        List<T> SelecionarTodos();

    }
}
