using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Mappers
{
    public static class FakturaToFakturaPozycjaXMLMapper
    {
        public static List<FakturaXMLViewModel> ToList<T>(this IEnumerable<Faktura> source)
        {
            List<FakturaXMLViewModel> mapped = source.Select(x => new FakturaXMLViewModel()
            {
                Numer = x.Numer,
                Pozycje = x.Pozycje.ToList<FakturaXMLViewModel>()
            }).ToList();

            return mapped;
        }

    }
}