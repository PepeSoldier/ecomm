using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MyMath
    {
        public static decimal Podziel(decimal a, decimal b, decimal c = 0)
        {
            if (b + c == 0) return 0;
            return a / (b + c);
        }
    }
}