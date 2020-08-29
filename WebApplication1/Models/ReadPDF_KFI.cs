using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Models
{
    public class ReadPDF_KFI : ReadPDF
    {
        public ReadPDF_KFI() : base()
        {

        }

        public override Faktura PutDataToInvoice(string theText)
        {
            Faktura faktura = new Faktura();
            Regex nrFakturyKFI = new Regex(@"[0-9]{0,}\/KFI\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            int index = 0;

            bool write = false;
            string[] theLines = theText.Split('\n');
            string temp = "";

            foreach (string theLine in theLines)
            {
                Match m = nrFakturyKFI.Match(theLine);
                if (m.Success)
                {
                    faktura.Numer = m.ToString();
                }

                if (theLine.IndexOf("według stawki VAT") >= 0)
                {
                    write = false;
                }

                if (write == true)
                {
                    if (Regex.IsMatch(theLine, @"^\d+ "))
                    {
                        temp += " " + theLine;
                        string[] substrings = temp.Split(' ');
                        string[] cenyZFakury = pobierzCenyZFaktury(substrings);

                        if (Regex.IsMatch(temp, @"^[0-9]{5,}"))
                        {
                            faktura.Pozycje.Add(new FakturaPozycja()
                            {
                                Lp = int.Parse(substrings[1]),
                                Symbol = GetSymbol(substrings),
                                Nazwa = string.Join(" ", substrings.Slice(2, -6)),
                                //Jm = substrings[substrings.Length - 8],
                                Ilosc = int.Parse(substrings[substrings.Length - 6]),
                                CenaJednostkowa = Convert.ToDecimal(cenyZFakury[0]),
                                VAT = int.Parse(substrings[substrings.Length - 4]),
                                WartoscNetto = Convert.ToDecimal(cenyZFakury[1]),
                                WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                                WartoscBrutto = Convert.ToDecimal(cenyZFakury[3])
                            });
                        }
                        else
                        {
                            if (Regex.IsMatch(temp, @"pakiet nr \d+"))
                            {
                                int value;
                                if (int.TryParse(substrings[substrings.Length - 4], out value))
                                {
                                    faktura.Pozycje.Add(new FakturaPozycja()
                                    {
                                        Nazwa = string.Join(" ", substrings.Slice(0, -11)),
                                        Jm = string.Join(" ", substrings.Slice(-11, -8)),
                                        Symbol = GetSymbol(substrings),
                                        CenaJednostkowa = Convert.ToDecimal(cenyZFakury[0]),
                                        VAT = int.Parse(substrings[substrings.Length - 6]),
                                        Lp = int.Parse(substrings[substrings.Length - 5]),
                                        Ilosc = int.Parse(substrings[substrings.Length - 4]),
                                        WartoscNetto = Convert.ToDecimal(cenyZFakury[1]),
                                        WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                                        WartoscBrutto = Convert.ToDecimal(cenyZFakury[3])
                                    });
                                }
                                else
                                {
                                    faktura.Pozycje.Add(new FakturaPozycja()
                                    {
                                        Nazwa = string.Join(" ", substrings.Slice(0, -11)),
                                        Jm = string.Join(" ", substrings.Slice(-11, -8)),
                                        Symbol = GetSymbol(substrings),
                                        VAT = int.Parse(substrings[substrings.Length - 7]),
                                        Lp = int.Parse(substrings[substrings.Length - 6]),
                                        Ilosc = int.Parse(substrings[substrings.Length - 5]),
                                        CenaJednostkowa = Convert.ToDecimal(cenyZFakury[0]),
                                        WartoscNetto = Convert.ToDecimal(cenyZFakury[1]),
                                        WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                                        WartoscBrutto = Convert.ToDecimal(cenyZFakury[3])
                                    });
                                }
                            }
                            else if ((index - 2) >= 0)
                            {
                                String[] substrings2 = theLines[index - 2].Split(' ');
                                if (Regex.IsMatch(theLines[index - 2], @"^pakiet nr \d+"))
                                {
                                    faktura.Pozycje.Add(new FakturaPozycja()
                                    {
                                        Nazwa = string.Join(" ", substrings.Slice(0, -8)),
                                        Symbol = GetSymbol(substrings2),
                                        Jm = string.Join(" ", substrings2.Slice(0, 3)),
                                        CenaJednostkowa = Convert.ToDecimal(cenyZFakury[0]),
                                        VAT = int.Parse(substrings[substrings.Length - 6]),
                                        Lp = int.Parse(substrings[substrings.Length - 5]),
                                        Ilosc = int.Parse(substrings[substrings.Length - 4]),
                                        WartoscNetto = Convert.ToDecimal(cenyZFakury[1]),
                                        WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                                        WartoscBrutto = Convert.ToDecimal(cenyZFakury[3])
                                    });
                                }
                                else
                                {
                                    faktura.Pozycje.Add(new FakturaPozycja()
                                    {
                                        Nazwa = string.Join(" ", substrings.Slice(0, -8)),
                                        Symbol = GetSymbol(substrings),
                                        CenaJednostkowa = Convert.ToDecimal(cenyZFakury[0]),
                                        VAT = int.Parse(substrings[substrings.Length - 6]),
                                        Lp = int.Parse(substrings[substrings.Length - 5]),
                                        Ilosc = int.Parse(substrings[substrings.Length - 4]),
                                        WartoscNetto = Convert.ToDecimal(cenyZFakury[1]),
                                        WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                                        WartoscBrutto = Convert.ToDecimal(cenyZFakury[3])
                                    });
                                }
                            }  
                        };
                    }
                    temp = theLine;
                }

                if (theLine.IndexOf("brutto netto VAT") >= 0)
                {
                    write = true;
                }
                index++;
            };
            return faktura;
        }

        private static string GetSymbol(string[] substrings)
        {
            Regex symbolRegex = new Regex("[0-9]{6,}");
            Match RegexMatch = symbolRegex.Match(string.Join(" ", substrings));
            return RegexMatch.Success ? RegexMatch.Value : "";
        }

        private string[] pobierzCenyZFaktury(string[] substrings)
        {
            string stringFaktura = string.Join(" ", substrings);
            MatchCollection matches = Regex.Matches(stringFaktura, @"\d+,\d{2}");
            string[] cenyZFaktury = matches.Cast<Match>().Select(m => m.Value).ToArray();
            return cenyZFaktury;
        }
    }
}