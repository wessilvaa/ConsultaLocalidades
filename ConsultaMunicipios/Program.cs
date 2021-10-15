using CEPLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMunicipios
{
    class Program
    {
        private delegate bool comparar(string a, string b);
        static void Main(string[] args)
        {
            Console.Title = "Consulta de Municipios - IBGE";
            try
            {
                //busca dados da api do ibge
                MsgAguardeConexao();
                var getFromAPI = new GetFromAPI();
                var ListaMunicipios = getFromAPI.GetMunicipios();


                //Pegunta se a busca vai ser exata
                Console.WriteLine("Deseja realizar uma consulta exata? S/N e aperte enter");
                var resposta = Console.ReadLine();
                while (resposta.ToLower() != "s" && resposta.ToLower() != "n")
                {
                    Console.WriteLine("Deseja realizar uma consulta exata? S/N e aperte enter");
                }


                //encapsula metodo de comparação
                comparar metodo = GetMetodoComparacao(resposta);
                Console.Clear();



                //aguarda o usuario digitar a cidade desejada
                MsgInsiraMunicipio();
                var municipio = Console.ReadLine();

                while (!string.IsNullOrEmpty(municipio) && municipio.Length >= 3)
                {
                    //busca a cidade aplicando o metodo de comparacao - exato ou nao
                    var listMunicipios = ListaMunicipios.Where(t => metodo(t.nome, municipio));

                    //imprime resultado no console
                    if (listMunicipios.Count() == 0)
                    {
                        Console.WriteLine($"Não encontramos municípios com o nome de {municipio}");
                    }
                    foreach (Municipio Municipio in listMunicipios)
                    {
                        EscreverMunicipioConsole(Municipio);
                    }


                    MsgInsiraMunicipio(true);

                    municipio = Console.ReadLine();

                    //caso usuario queira limpar o console
                    while (municipio.ToLower() == "c")
                    {
                        Console.Clear();
                        MsgInsiraMunicipio();
                        municipio = Console.ReadLine();

                    }
                    //se o o usuario desejar sair da aplicação
                    if (municipio.ToLower() == "q") { Environment.Exit(0); }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu o seguinte erro durante a execução: {ex.Message}");
            }

        }

        private static comparar GetMetodoComparacao(string resposta)
        {
            comparar metodo;
            if (resposta.ToLower() == "s")
            {
                metodo = ((a, b) => string.Compare(a.ToLower(), b.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);
            }
            else
            {
                metodo = ((a, b) => a.ToLower().ContainsInsensitive(b.ToLower()));
            }
            return metodo;
        }

        private static void MsgAguardeConexao()
        {
            Console.WriteLine("Por favor, aguarde enquanto iniciamos a conexão com o IBGE...");
        }

        private static void MsgInsiraMunicipio(bool msgExtra = false)
        {
            Console.WriteLine("\nPor favor insira a cidade e aperte enter: ");
            if (msgExtra)
            {
                Console.WriteLine("---Insira ”C” para limpar o console e ”Q” para encerrar.---");
            }
        }

        private static void EscreverMunicipioConsole(Municipio municipio)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Municipo: {municipio.nome}");
            Console.WriteLine($"Microregiao: {municipio.microrregiao.nome}");
            Console.WriteLine($"Mesoregiao: {municipio.microrregiao.mesorregiao.nome}");
            Console.WriteLine($"UF: {municipio.microrregiao.mesorregiao.UF.sigla}");
            Console.WriteLine($"Estado: {municipio.microrregiao.mesorregiao.UF.nome}");
            Console.WriteLine($"Regiao: {municipio.microrregiao.mesorregiao.UF.regiao.nome}");
        }
    }
}
