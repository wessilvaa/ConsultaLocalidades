using System.Collections.Generic;

namespace CEPLibrary
{
    public interface IGetFromAPI
    {
        IEnumerable<Municipio> GetMunicipio(string municipio);
        IEnumerable<Municipio> GetMunicipios();
    }
}