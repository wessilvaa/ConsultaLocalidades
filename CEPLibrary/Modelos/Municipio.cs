using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEPLibrary
{
    public class Municipio
    {
        public int id { get; set; }
        public string nome { get; set; }
        public Microregiao microrregiao { get; set; }
    }
}
