using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Mappers
{
    public static class FakturaPozycjaToFakturaPozycjaXMLMapper
    {
        public static List<FakturaPozycjaXMLViewModel> ToList<T>(this IEnumerable<FakturaPozycja> source)
        {
            List<FakturaPozycjaXMLViewModel> mapped = source.Select(x => new FakturaPozycjaXMLViewModel()
            {
                KodArtykulu = x.Symbol,
                WartoscBrutto = x.WartoscBrutto,
                WartoscNetto = x.WartoscNetto,
                WartoscVAT = x.WartoscVAT
            }).ToList();

            return mapped;
        }

    }
}