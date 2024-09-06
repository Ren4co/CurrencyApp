using CurrencyApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Domain.Entity
{
    public class Money
    {
        public decimal Value { get; set; }

        public Currency Currency { get; set; }

    }
}
