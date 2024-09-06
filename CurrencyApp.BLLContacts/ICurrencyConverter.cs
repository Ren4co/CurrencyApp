using CurrencyApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.BLLContacts
{
    public interface ICurrencyConverter
    {
        public decimal Convert(decimal Value, Currency cur1, Currency cur2, IDictionary<(Currency, Currency), decimal> exchangeRates);
    }
}
