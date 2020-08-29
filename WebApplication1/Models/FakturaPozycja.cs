using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FakturaPozycja
    {
        public int Id { get; set; }

        public Faktura Faktura { get; set; }
        public int FakturaId { get; set; }

        public int Lp { get; set; }
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
        public string PKWiU { get; set; }
        public string Jm { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public int VAT { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscVAT { get; set; }
        public decimal WartoscBrutto { get; set; }
        //public string Text { get; set; }
    }
}