using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.ViewModels
{
    [XmlType("Pozycja")]
    public class FakturaPozycjaXMLViewModel
    {
        public string KodArtykulu { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscVAT { get; set; }
        public decimal WartoscBrutto { get; set; }
    }
}