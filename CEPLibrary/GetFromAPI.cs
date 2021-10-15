using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CEPLibrary
{
    public class GetFromAPI : IGetFromAPI
    {
        private string ApiMunicipio = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios/";
        public IEnumerable<Municipio> GetMunicipio(string municipio)
        {
            if (!String.IsNullOrEmpty(municipio))
            {
                var client = new RestClient(ApiMunicipio);
                var request = new RestRequest(municipio, Method.GET);
                var response = client.Execute(request);
                var output = new List<Municipio>();


                if (IsArray(response.Content))
                {
                    output = JsonConvert.DeserializeObject<List<Municipio>>(response.Content);
                }
                else if (response.Content.Length > 3)
                {
                    var mcp = JsonConvert.DeserializeObject<Municipio>(response.Content);
                    output.Add(mcp);
                }
                return output;
            }
            else
            {
                throw new Exception("Municipio em branco. Nao é possivel realizar a consulta.");
            }
        }
        public IEnumerable<Municipio> GetMunicipios()
        {

            var client = new RestClient(ApiMunicipio);
            var request = new RestRequest("", Method.GET);
            var response = client.Execute(request);
            var output = new List<Municipio>();
            if (IsArray(response.Content))
            {
                output = JsonConvert.DeserializeObject<List<Municipio>>(response.Content);
            }
            else if (response.Content.Length > 3)
            {
                var mcp = JsonConvert.DeserializeObject<Municipio>(response.Content);
                output.Add(mcp);
            }
            return output;

        }
        private bool IsArray(string content)
        {
            if (content.Substring(0, 1) == "[" && content.Substring(content.Length - 1) == "]")
            {
                return true;
            }
            return false;
        }
    }
}
