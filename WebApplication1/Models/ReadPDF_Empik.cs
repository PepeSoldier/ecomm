using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Models
{
    public class ReadPDF_Empik : ReadPDF
    {
        public ReadPDF_Empik() : base()
        {

        }

        public override Faktura PutDataToInvoice(string theText)
        {
            Faktura faktura = new Faktura();
            Regex nrFakturyEmpik = new Regex(@"FS\/[0-9]{0,}\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool write = false;
            string[] theLines = theText.Split('\n');

            foreach (string theLine in theLines)
            {

                Match m = nrFakturyEmpik.Match(theLine);
                if (m.Success)
                {
                    faktura.Numer = m.ToString();
                }


                if (theLine.IndexOf("Razem:") >= 0)
                {
                    write = false;
                }

                if (write == true)
                {
                    String[] substrings = theLine.Split(' ');

                    if (substrings[1] != "%")
                    {
                        string[] cenyZFaktury = pobierzCenyZFaktury(substrings);
                        faktura.Pozycje.Add(new FakturaPozycja()
                        {
                            Lp = int.Parse(substrings[0]),
                            Symbol = substrings[1],
                            Nazwa = string.Join(" ", substrings.Slice(2, -8)),
                            Jm = substrings[substrings.Length - 8],
                            Ilosc = int.Parse(substrings[substrings.Length - 7]),
                            CenaJednostkowa = Convert.ToDecimal(cenyZFaktury[0]),
                            VAT = int.Parse(substrings[substrings.Length - 5]),
                            WartoscNetto = Convert.ToDecimal(cenyZFaktury[1]),
                            WartoscVAT = Convert.ToDecimal(cenyZFaktury[2]),
                            WartoscBrutto = Convert.ToDecimal(cenyZFaktury[3])
                        });
                    }
                }

                if ( theLine.IndexOf("towaru / usługi") >= 0 )
                {
                    write = true;
                }

            };
            return faktura;
        }

        private string[] pobierzCenyZFaktury(string[] substrings)
        {
            string stringFaktura = string.Join(" ", substrings);
            MatchCollection matches = Regex.Matches(stringFaktura, @"\d+,\d{2}");
            string[] cenyZFaktury =  matches.Cast<Match>().Select(m => m.Value).ToArray();
            return cenyZFaktury;
        }
    }
}