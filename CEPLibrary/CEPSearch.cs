using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CEPLibrary
{

    public class CEPSearch : ICEPSearch
    {
        private delegate bool comparar(string a, string b);

        public IEnumerable<Municipio> Pesquisar(IEnumerable<Municipio> lista, string textoPesquisa, bool pesquisaExata)
        {
            var metodo = GetMetodoComparacao(pesquisaExata);
            var listaResultado = lista.Where(t => metodo(t.nome, textoPesquisa));
            return listaResultado;
        }

        private comparar GetMetodoComparacao(bool pesquisaExata)
        {
            comparar metodo;
            if (pesquisaExata)
            {
                metodo = ((a, b) => string.Compare(a.ToLower(), b.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);
            }
            else
            {
                metodo = ((a, b) => a.ToLower().ContainsInsensitive(b.ToLower()));
            }
            return metodo;
        }
    }

}
namespace System
{
    public static class StringExt
    {
        public static bool ContainsInsensitive(this string source, string search)
        {
            return (new CultureInfo("pt-BR").CompareInfo).IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
        }
    }
}