using CurrencyApp.DALContracts;
using CurrencyApp.Domain.Enum;

namespace CurrencyApp.DAL
{
    public class CurrencyDao : ICurrencyDao
    {
        public IDictionary<(Currency, Currency), decimal> GetCurrencyRates()
        {
            var exchangeRates = new Dictionary<(Currency, Currency), decimal>
            {
                { (Currency.USD, Currency.EUR), 0.85m },
                { (Currency.EUR, Currency.USD), 1.18m },
                { (Currency.USD, Currency.GBR), 0.74m },
                { (Currency.GBR, Currency.USD), 1.35m },
                /*{ (Currency.EUR, Currency.GBR), 0.87m },*/
                { (Currency.GBR, Currency.EUR), 1.15m }
            };

            return exchangeRates;

        }
    }
}
