using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.ViewModels
{
    [XmlType("Faktura")]
    public class FakturaXMLViewModel
    {
        //public FakturaXMLViewModel()
        //{
        //    //Pozycje = new List<FakturaPozycjaXMLViewModel>();
        //}

        //public FakturaXMLViewModel(string symbol)
        //{

        //    //Pozycje = new List<FakturaPozycjaXMLViewModel>();
        //    //Pozycje.Add(new FakturaPozycjaXMLViewModel() { KodArtykulu = symbol });
        //}

        public string Numer { get; set; }
        public List<FakturaPozycjaXMLViewModel> Pozycje { get; set; }
    }
}