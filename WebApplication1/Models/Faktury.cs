using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Faktury
    {
        public Faktury()
        {
            ListaFaktur = new List<Faktura>();
        }
        public List<Faktura> ListaFaktur { get; set; }
    }
}