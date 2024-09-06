using CurrencyApp.BLLContacts;
using CurrencyApp.DALContracts;
using CurrencyApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.BLL
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public CurrencyConverter()
        {
            
        }

        public decimal Convert(decimal value, Currency fromCurrency, Currency toCurrency, IDictionary<(Currency, Currency), decimal> exchangeRates)
        {
            if(fromCurrency == toCurrency)
            {
                return value;
            }

            if(exchangeRates.TryGetValue((fromCurrency, toCurrency), out var rate))
            {
                return value * rate;
            }
            else
            {
                foreach (var intermediateCurrency in Enum.GetValues(typeof(Currency)))
                {
                    if (intermediateCurrency.Equals(fromCurrency) || intermediateCurrency.Equals(toCurrency))
                    {
                        continue;
                    }

                    if (exchangeRates.TryGetValue((fromCurrency, (Currency)intermediateCurrency), out var rateFromToIntermediate) &&
                        exchangeRates.TryGetValue(((Currency)intermediateCurrency, toCurrency), out var rateIntermediateTo))
                    {
                        return value * rateFromToIntermediate * rateIntermediateTo;
                    }
                }
            }
            throw new InvalidOperationException("Конвертация недоступна");

        }
    }
}
