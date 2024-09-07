using CurrencyApp.Domain.Entity;
using CurrencyApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.BLLContacts
{
    public interface IMoneyOperation
    {
        Money Add(Money money1, Money money2, Currency destinationCurrency);
        Money Subtract(Money money1, Money money2, Currency destinationCurrency);
        decimal Convert(Currency fromCurrency, Currency toCurrancy, decimal Value);
    }
}
