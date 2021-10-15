using System.Collections.Generic;

namespace CEPLibrary
{
    public interface ICEPSearch
    {
        IEnumerable<Municipio> Pesquisar(IEnumerable<Municipio> lista, string textoPesquisa, bool pesquisaExata);
    }
}