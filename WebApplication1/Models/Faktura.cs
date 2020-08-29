using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    public class Faktura
    {
        public Faktura()
        {
            Pozycje = new List<FakturaPozycja>();
        }
        public int Id { get; set; }
        [MaxLength(35)]
        public string Numer { get; set; }
        public virtual List<FakturaPozycja> Pozycje { get; set; }
    }
}